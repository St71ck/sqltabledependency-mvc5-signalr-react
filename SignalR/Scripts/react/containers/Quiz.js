import React, { Component } from 'react';
import axios from 'axios';
import { hubConnection } from 'signalr-no-jquery';

import RadioInput from '../components/RadioInput';

class Quiz extends Component {
    state = {
        current_quiz: { question: '', choices: [] },
        user_choice: "",
        is_done: false,
        usuarioId: 0
    }

    componentDidMount() {
        axios.get('/home/surveyquiz')
            .then(response => {
                this.setState({
                    current_quiz: response.data,
                    user_choice: "",
                    is_done: false
                });
            })
            .catch(error => {
                console.log(error);
            });
    }

    onSelectedAnswerHandler = (option) => {
        this.setState({ user_choice: option });
    }

    onSubmitHandler = () => {
        const frutaNombre = this.state.user_choice;
        const options = { useDefaultPath: false };
        const connection = hubConnection('http://localhost:36403/signalr', options);
        connection.start({ jsonp: false })
            .done(function () {

                axios.get('/home/RegistrarVotoUsuario', {
                    params: {
                        pUsuario: connection.id,
                        pFruta: frutaNombre
                    }
                })
                    .then(response => {
                        this.setState({
                            is_done: true
                        });
                    })
                    .catch(error => {
                        console.log(error);
                    });

                console.log('Now connected, connection ID=' + connection.id);
            })
            .fail(function () { console.log('Could not connect'); });





        this.setState({ is_done: true });
    }

    /* onSubmitHandler = () => {
        let $this = this;
        const options = { useDefaultPath: false };
        const connection = hubConnection('http://localhost:36403/signalr', options);
        const hubProxy = connection.createHubProxy('frutaHub');

        connection.start({ jsonp: false })
            .done(function () {
                console.log('Now connected, connection ID=' + connection.id);
                hubProxy.invoke('EnviarVoto', $this.state.user_choice);
            })
            .fail(function () { console.log('Could not connect'); });

        this.setState({ is_done: true });
    } */

    render() {
        const self = this;
        if (this.state.is_done) {
            return (
                <div className="quizContainer">
                    <h1>Gracias por su voto!!. </h1>
                </div>
            );
        }
        else {
            var choices = this.state.current_quiz.choices.map(function (choice, index) {
                return (
                    <RadioInput key={choice.name} choice={choice.name} index={index} onChoiceSelect={self.onSelectedAnswerHandler} />
                );
            });

            return (
                <div className="quizContainer">
                    <h1>Quiz</h1>
                    <p>{this.state.current_quiz.question}</p>
                    {choices}
                    <button id="submit" className="btn btn-default" onClick={this.onSubmitHandler}>Enviar</button>
                </div>
            );
        }
    }

}

export default Quiz;