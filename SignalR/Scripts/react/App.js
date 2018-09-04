import React, { Component } from 'react';
import { Route, Switch, Link } from 'react-router-dom';

import Quiz from './containers/Quiz';
import Dashboard from './containers/Dashboard';

class App extends Component {
    render() {
        return (
            <div>
                <Link to="/home/quiz">Quiz</Link> <br />
                <Link to="/home/dashboard">Dashboard</Link>
                <Switch>
                    <Route exact path="/home/quiz" component={Quiz} />
                    <Route exact path="/home/dashboard" component={Dashboard} />
                    <Route exact path="/" component={Quiz} />
                    <Route component={Quiz} />
                </Switch>
            </div>
        );
    }
}

export default App;
