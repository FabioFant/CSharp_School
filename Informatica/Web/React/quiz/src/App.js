import './App.css';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap/dist/js/bootstrap.js';
import Header from './components/Header';
import Footer from './components/Footer';
import Quiz from './components/Quiz';
import { Component } from 'react';

class App extends Component {
  state = {
    data: [],
  }

  render(){
    return (
      <div className="App">
        <Header/>
        <div className='container'>
          {
            this.state.data.map((quiz) => {
              return (
                <div className="row">
                  <div className='col'>
                    <Quiz question={quiz.question} options={quiz.options}/>
                  </div>
                </div>
                )
            })
          }
          <Footer/>
        </div>
      </div>
    );
  }

  async componentDidMount(){
    await this.loadData();
  }

  async loadData(){
    const response = await fetch('domande.json');
    const json = await response.json();
    this.setState({data: json});
  }
}

export default App;
