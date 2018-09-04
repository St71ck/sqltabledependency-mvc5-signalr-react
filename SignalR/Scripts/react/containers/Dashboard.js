import React, { Component } from 'react';
import axios from 'axios';
import { hubConnection } from 'signalr-no-jquery';

import PieChart from '../components/PieChart';

class Dashboard extends Component {
    state = {
        data: [],
        sucursales: [1, 3],
        datosCambiaron: false
    }

    componentDidMount() {
        var $this = this;
        this.startInterval();
        this.ObtenerFrutas();

        const options = { useDefaultPath: false };
        const connection = hubConnection('http://localhost:36403/signalr', options);
        const hubProxy = connection.createHubProxy('frutaHub');

        /* hubProxy.on('enviarMensaje', data => {
            //const obj = JSON.parse(data);
            if (this.state.sucursales.indexOf(data.sucursalId) != -1) {
                //this.ObtenerFrutas();
                this.setState({ datosCambiaron: true });
            }
            
            console.log(data);
        }); */

        hubProxy.on('enviarMensaje', data => {

            if (this.state.data) {
                let datos = this.state.data;

                const frutas = datos.map(fruta => {
                    return { name: fruta.name, y: fruta.y + (fruta.name === data.Fruta ? 1 : 0)};
                });

                this.setState({ data: frutas });
                console.log(frutas);
            }

            console.log(data);
        });

        connection.start({ jsonp: false })
            .done(function () {
                console.log('Now connected, connection ID=' + connection.id);
            })
            .fail(function () { console.log('Could not connect'); });
    }

    componentWillReceiveProps() {
        this.stopInterval();
        this.startInterval();
    }

    componentWillUnmount() {
        this.stopInterval();
    }

    startInterval() {
        this.timer = setInterval(() => {
            if (this.state.datosCambiaron) {
                this.ObtenerFrutas();
            }
        }, 30000);
    }

    stopInterval() {
        if (this.timer) {
            clearInterval(this.timer)
            this.timer = null
        }
    }

    ObtenerFrutas = () => {
        axios.get("/home/surveyquiz")
            .then(response => {
                let datos = [];
                if (response.data.choices) {
                    datos = response.data.choices.map(fruta => {
                        return { name: fruta.name, y: fruta.count };
                    });
                }
                this.setState({
                    data: datos,
                    datosCambiaron: false
                });
            })
            .catch(error => {
                console.log(error);
            });
    }

    render() {
        return (
            <div className="dashboardapp">
                {this.state.data ?
                    <PieChart datos={this.state.data} />
                    : null
                }
            </div>
        );
    }
}

export default Dashboard;