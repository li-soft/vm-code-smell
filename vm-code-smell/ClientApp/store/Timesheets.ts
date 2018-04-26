import { fetch, addTask } from 'domain-task';
import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface TimesheetsState {
    entries: TimesheetEntry[];
}

export interface TimesheetEntry {
    entryId: number;
    dateFormatted: string;
    timeSpent: number;
    projectId: number;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestTimesheetsAction {
    type: 'REQUEST_TIMESHEETS';
}

interface ReceiveTimesheetsAction {
    type: 'RECEIVE_TIMESHEETS';
    entries: TimesheetEntry[];
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestTimesheetsAction | ReceiveTimesheetsAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestTimesheets: (): AppThunkAction<KnownAction> => (dispatch) => {
        let fetchTask = fetch(`api/Timesheet/GetAll`)
            .then(response => response.json() as Promise<TimesheetEntry[]>)
            .then(data => 
                dispatch({ type: 'RECEIVE_TIMESHEETS', entries: data })
            );

        addTask(fetchTask); // Ensure server-side prerendering waits for this to complete
        dispatch({ type: 'REQUEST_TIMESHEETS' });
    },

    deleteTimesheet: (entryId: number): AppThunkAction<KnownAction> => 
        (dispatch) => {
        let request = new Request(`api/Timesheet/DeleteTimesheetEntry?id=${ entryId }`);
            let deleteTask = fetch(request);

            addTask(deleteTask); // Ensure server-side prerendering waits for this to complete

            let fetchTask = fetch(`api/Timesheet/GetAll`)
                .then(response => response.json() as Promise<TimesheetEntry[]>)
                .then(data =>
                    dispatch({ type: 'RECEIVE_TIMESHEETS', entries: data })
                );

            addTask(fetchTask); // Ensure server-side prerendering waits for this to complete
            dispatch({ type: 'REQUEST_TIMESHEETS' });
        },
    
    addNewEntry: (): AppThunkAction<KnownAction> => (dispatch) => {
        let request = new Request(`api/Timesheet/AddNew`);
        let newEntryTask = fetch(request);

        addTask(newEntryTask); // Ensure server-side prerendering waits for this to complete

        let fetchTask = fetch(`api/Timesheet/GetAll`)
            .then(response => response.json() as Promise<TimesheetEntry[]>)
            .then(data =>
                dispatch({ type: 'RECEIVE_TIMESHEETS', entries: data })
            );

        addTask(fetchTask); // Ensure server-side prerendering waits for this to complete
        dispatch({ type: 'REQUEST_TIMESHEETS' });
}
    
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: TimesheetsState = { entries: [] };

export const reducer: Reducer<TimesheetsState> = (state: TimesheetsState, incomingAction: Action) => {
    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_TIMESHEETS':
            return {
                entries: state.entries
            };
        case 'RECEIVE_TIMESHEETS':
            // Only accept the incoming data if it matches the most recent request. This ensures we correctly
            // handle out-of-order responses.
                return {
                    entries: action.entries
                };
        default:
            // The following line guarantees that every action in the KnownAction union has been covered by a case above
            const exhaustiveCheck: never = action;
    }

    return state || unloadedState;
};
