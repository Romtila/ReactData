import React from "react";
import { Bar } from "@reactchartjs/react-chart.js";

import { connect } from "react-redux";

const options = {
    scales: {
        yAxes: [
            {
                ticks: {
                    beginAtZero: true,
                },
            },
        ],
    },
};

const RollingRetention7Day = ({ rollingRetention7Day }) => {
    const data = {
        labels: rollingRetention7Day.map((data) => data.userLifespan + " дней"),
        datasets: [
            {
                label: "Количество",
                data: rollingRetention7Day.map((data) => data.count),
                backgroundColor: "rgba(54, 162, 235, 1)",
                borderColor: "rgba(54, 162, 235, 1)",
                borderWidth: 1,
            },
        ],
    };
    return (
        <div>
            <h2 className='text-secondary'>Гистрограмма распределения длительности жизней пользователя:</h2>
            <Bar data={data} options={options} />
        </div>
    );
};

const mapStateToProps = (state) => {
    return {
        rollingRetention7Day: state.dataRollingRetention.rollingRetention7Day,
    };
};

export default connect(mapStateToProps)(RollingRetention7Day);
