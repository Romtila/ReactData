import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Link, NavLink } from 'react-router-dom';
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";

interface FetchUserDataState {
    empList: UserData[];
    loading: boolean;
}

export class FetchUser extends React.Component<RouteComponentProps<{}>, FetchUserDataState> {
    constructor() {
        super();
        this.state = { empList: [], loading: true, dateReg: new Date(), dateLastAct: new Date() };
        this.handleSave = this.handleSave.bind(this);
        this.handleChangeDateReg = this.handleChangeDateReg.bind(this);
        this.handleChangeDateLastAct = this.handleChangeDateLastAct.bind(this);
        this.handleCalculate = this.handleCalculate.bind(this);
    }

    handleChangeDateReg(date) {
        this.setState({
            dateReg: date
        })
    }

    handleChangeDateLastAct(date) {
        this.setState({
            dateLastAct: date
        })
    }

    handleCalculate() {
        console.log("hello world");
    }

    componentDidMount() {
        fetch('Users/Users')
            .then(response => response.json() as Promise<UserData[]>)
            .then(data => {
                console.log(data);
                this.setState({ empList: data, loading: false })
            }
            );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderUserTable(this.state.empList);

        return <div>
            <p>
                {contents}
            </p>
            <button className="btn" onClick={this.handleCalculate}>Calculate</button>
        </div>;
    }

    private handleSave(event) {
        event.preventDefault();
        const data = new FormData(event.target);
        fetch('Users/SaveUser', {
            method: 'POST',
            body: data,

        }).then((response) => response.json())
            .then((responseJson) => {
                this.props.history.push("/fetchuser");
            })
        console.log(event);
    }

    // Returns the HTML table to the render() method.  
    renderUserTable(empList: UserData[]) {
        console.log(empList);
        return <form onSubmit={this.handleSave} >
            <table className='table'>
                <thead>
                    <tr>
                        <th></th>
                        <th>UserID</th>
                        <th>Date Registration</th>
                        <th>Date Last Activity</th>
                    </tr>
                </thead>
                <tbody>
                    {this.state.empList.map(emp =>
                        <tr key={emp.employeeId}>
                            <td></td>
                            <td>{emp.id}</td>
                            <td>{emp.dateRegistration}</td>
                            <td>{emp.dateLastActivity}</td>
                        </tr>
                    )}
                    <tr>
                        <td></td>
                        <td><div className="form-group row" >
                            <label className="control-label col-md-8" >Add new user:</label>
                        </div></td>
                        <td>< div className="form-group row" >
                            <div className="col-md-5">
                                <DatePicker
                                    selected={this.state.dateReg}
                                    onChange={this.handleChangeDateReg}
                                    className="form-control"
                                    name="dateRegistration"
                                    defaultValue={this.state.empList.dateRegistration}
                                    dateFormat="yyyy/MM/dd"
                                    required
                                />
                            </div>
                        </div ></td>
                        <td><div className="col-md-5">
                            <DatePicker
                                selected={this.state.dateLastAct}
                                onChange={this.handleChangeDateLastAct}
                                className="form-control"
                                name="dateRegistration"
                                defaultValue={this.state.empList.dateLastActivity}
                                dateFormat="yyyy/MM/dd"
                                required
                            />
                        </div>
                        </td>
                    </tr>
                </tbody>
            </table>
            <div className="form-group">
                <button type="submit" className="btn btn-default">Save</button>
                <button className="btn" Roundness onClick={this.handleCalculate}>Calculate</button>
            </div >
        </form >;
    }
}

export class UserData {
    ID: number = 0;
    DateRegistration: string = "";
    DateLastActivity: string = "";
}    