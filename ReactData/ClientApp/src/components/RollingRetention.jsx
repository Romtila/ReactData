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
import React from "react";

const RollingRetention = ({ loading = false, datesUsers, alertErrorRR }) => {
    const dispath = useDispatch();

    const [post, setPost] = React.useState(null);

    const loadCalculateRRHandle = () => {
        loading = true;
        dispath(calculateData7DayFromDB());
    };    

    React.useEffect(() => {
      axios.get("api/rollingretention/calculaterollingretention").then((response) => {
        setPost(response.data);
      });
    }, []);
    
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
            {loading ? (
                        <h3 className='text-secondary'>Rolling Retention 7 day =</h3>
                    ) : (
                        <h3 className='text-secondary'>Rolling Retention 7 day = {post + " %"}</h3>
                        )}

            <div className='container p-5 pt-3'>
                <RollingRetention7Day />
            </div>
        </div>
    );
};

const mapStateToProps = (state) => {
    return {
        loading: state.app.loading,
        datesUsers: state.datesUsers.datesUser,
        alertErrorRR: state.app.alertErrorRR,
    };
};



export default connect(mapStateToProps)(RollingRetention);
