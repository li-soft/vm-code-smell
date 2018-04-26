import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState }  from '../store';
import * as TimesheetState from '../store/Timesheets';

type TimesheetProps =
    TimesheetState.TimesheetsState
    & typeof TimesheetState.actionCreators
    & RouteComponentProps<{ startDateIndex: string }>;

class Timesheet extends React.Component<TimesheetProps, {}> {
    componentWillMount() {
        this.props.requestTimesheets();
    }

    public render() {
        let currnetMonth = new Date().getMonth();
        let currentYear = new Date().getFullYear();
        return <div>
            <h1>Period: { currnetMonth }.{ currentYear }</h1>
            { this.renderTable() }
            <button className='btn btn-success' onClick={e => this.addNewEntry()}>Add new</button>
        </div>;
    }

    private renderTable() {
        return <table className='table'>
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Date</th>
                    <th>Time [h]</th>
                    <th>Project</th>
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody>
            {this.props.entries.map(entry =>
                <tr key={ entry.entryId }>
                    <td>{ entry.entryId }</td>
                    <td>{ entry.dateFormatted }</td>
                    <td>{ entry.timeSpent }</td>
                    <td>{ entry.projectId }</td>
                    <td> 
                        <button className='btn btn-danger' onClick={e => this.deleteEntry(entry.entryId) }>Delete</button>
                    </td>
                </tr>
            )}
            </tbody>
        </table>;
    }

    private renderPagination() {
        let prevStartDateIndex = 5;
        let nextStartDateIndex = 5;

        return <p className='clearfix text-center'>
            <Link className='btn btn-default pull-left' to={ `/fetchdata/${ prevStartDateIndex }` }>Previous</Link>
            <Link className='btn btn-default pull-right' to={ `/fetchdata/${ nextStartDateIndex }` }>Next</Link>
        </p>;
    }

    private deleteEntry(entryId: number) {
        this.props.deleteTimesheet(entryId);
    }

    private addNewEntry() {
        this.props.addNewEntry();
    }
}

export default connect(
    (state: ApplicationState) => state.timesheets, 
    TimesheetState.actionCreators
)(Timesheet) as typeof Timesheet;
