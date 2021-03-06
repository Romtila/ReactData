import { connect } from "react-redux";
import { useDispatch } from "react-redux";
import './style.css';

import {
    submitDates,
    showAlertWarning,
    hideAlertWarning,
    hideAlertError,
    hideAlertSucces,
} from "../redux/actions";
import {
    TableRow,
    AlertWarning,
    ButtonAddRow,
    Loader,
    AlertSucces,
    AlertError,
} from "./index";

const DateTable = ({
    datesUsers,
    loading,
    alertWarning,
    alertError,
    alertSucces,
}) => {
    const dispath = useDispatch();

    const submitHandle = (event) => {
        event.preventDefault();

        let isValid = true;
        let id = "";
        let message = "";

        dispath(hideAlertError());
        dispath(hideAlertSucces());

        datesUsers.map((datesUser) => {
            if (datesUser.dateLastActivity < datesUser.dateRegistration) {
                id = datesUser.id;
                isValid = false;
                message =
                    "The last activity date cannot be earlier than the registration date. Please check the entered data with the user ( ID = " +
                    id +
                    " )";
            }

            if (
                datesUser.dateLastActivity == "" ||
                datesUser.dateRegistration == ""
            ) {
                id = datesUser.id;
                isValid = false;
                message = "Please enter dates for the user ( ID = " + id + " )";
            }
        });

        if (!isValid) {
            dispath(showAlertWarning(message));
            return;
        } else dispath(hideAlertWarning());

        dispath(submitDates(datesUsers));
    };

    return (
        <div className='container p-5'>
            <div className='container p-5 pt-3'>
                <form>
                    <table className='table'>
                        <thead>
                            <tr>
                                <th scope='col' className='text-secondary'>
                                    ID
                                </th>
                                <th scope='col' className='text-secondary'>
                                    DATE REGISTRATION
                                </th>
                                <th scope='col' className='text-secondary'>
                                    DATE LAST ACTIVITY
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            {datesUsers.map((date) => (
                                <TableRow key={date.id} dateUser={date} />
                            ))}
                        </tbody>
                    </table>

                    {alertSucces && <AlertSucces text={alertSucces} />}
                    {alertWarning && <AlertWarning text={alertWarning} />}
                    {alertError && <AlertError text={alertError} />}

                    {!loading ? (
                        <button
                            type='button'
                            className='btn btn-primary'
                            onClick={submitHandle}>
                            Save
                        </button>
                    ) : (
                            <Loader />
                        )}
                </form>
            </div>
        </div>
    );
};

const mapStateToProps = (state) => {
    return {
        datesUsers: state.datesUsers.datesUser,
        loading: state.app.loading,
        alertError: state.app.alertError,
        alertSucces: state.app.alertSucces,
        alertWarning: state.app.alertWarning,
    };
};

export default connect(mapStateToProps)(DateTable);
