import React, { Component } from 'react';
import { Header, Icon, List } from 'semantic-ui-react'
import './App.css';
//axios paketi kurulumdan sonra ilgili componente import edilir.
import axios from 'axios';




class App extends Component {

  // compoenete values isminde dizi şeklinde değer tutan bir state tanımladık
  // bu state nesnesi her değiştiğinde sayfa render edilir.
  state = {
    values: []
  }

  // api ile iletişim için axios isimli paketi yükledik.
  componentDidMount() {

    axios.get('http://localhost:5000/api/values')
      .then((response) => {
        //console.log(response);
        this.setState({
          values: response.data
        })
      })
  }

  render() {
    return (
      <div>
        <Header as='h2'>
          <Icon name='plug' />
          <Header.Content>Reactivities</Header.Content>
        </Header>

        <List>
          {this.state.values.map((value: any) => (
            // <li key={value.id}> {value.name}</li>
            <List.Item key={value.id}> {value.name} </List.Item>
          ))}

        </List>
      </div>
    );
  }
}

export default App;
