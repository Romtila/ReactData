import { RollingRetention7Day, RollingRetentionXDay } from "./index";
import { useDispatch } from "react-redux";
import { connect } from "react-redux";

import { AlertError } from "./index";
import {
    calculateData7DayFromDB,
    calculateData7DayFromClient,
    calculateDataXDayFromDB,
    calculateDataXDayFromClient,
    calculateRollingRetention,
} from "../redux/actions";
import axios from "axios";

const RollingRetention = ({ datesUsers, alertErrorRR }) => {
    const dispath = useDispatch();

    const laodFromDBHandle = () => {
        dispath(calculateDataXDayFromDB());
        dispath(calculateData7DayFromDB());
    };

    const laodFromClientHandle = () => {
        dispath(calculateDataXDayFromClient(datesUsers));
        dispath(calculateData7DayFromClient(datesUsers));
    };

const loadCalculateRRHandle = () => {
        dispath(calculateRollingRetention(datesUsers));
        dispath(calculateData7DayFromDB());
    };

    return (
        <div className='container p-5'>
            <div className='container p-5 pt-1'>
                {alertErrorRR && <AlertError text={alertErrorRR} />}

                <button
                    type='button'
                    className='btn btn-primary'
                    onClick={loadCalculateRRHandle}>
                    Calculate
                </button>
            </div>
            <h3 className='text-secondary'>Rolling Retention 7 day = {}%</h3>

            <div className='container p-5 pt-3'>
                <RollingRetention7Day />
            </div>
        </div>
    );
};

const mapStateToProps = (state) => {
    return {
        datesUsers: state.datesUsers.datesUser,
        alertErrorRR: state.app.alertErrorRR,
    };
};



export default connect(mapStateToProps)(RollingRetention);
