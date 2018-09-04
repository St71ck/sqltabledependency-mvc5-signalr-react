import React from 'react';
import Highcharts from 'highcharts';
import {
  HighchartsChart, Chart, withHighcharts, XAxis, YAxis, Title, Legend, PieSeries, Tooltip
} from 'react-jsx-highcharts';

const pieChart = props => (
    <div>
        <HighchartsChart>
            <Chart height={700} width={700}  />
            <Title>Votación de Frutas</Title>
            <Legend />
            <XAxis categories={props.datos.map(a => a.name)} />
            <YAxis min='0' allowDecimals={false}>
                <PieSeries name="Total votos" data={props.datos} showInLegend={false} />
            </YAxis>
            <Tooltip />
        </HighchartsChart>
    </div>
);

export default withHighcharts(pieChart, Highcharts);