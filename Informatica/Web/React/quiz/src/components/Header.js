import { Component } from 'react';

class Header extends Component {
    render(){
        return(
            <nav className="navbar bg-body-tertiary">
                <div className="container-fluid">
                    <a className="navbar-brand" href="#">
                        <img src="/images/F_Sharp_logo.svg" alt="Logo" width="30" height="24" className="d-inline-block align-text-top"/>
                        Quiz
                    </a>
                </div>
            </nav>
        )
    }
}

export default Header;