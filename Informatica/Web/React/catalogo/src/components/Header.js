import { Component } from "react";

class Navbar extends Component {
    render() {
        return(
            <nav className ="navbar bg-body-tertiary">
                <div className ="container-fluid">
                    <a className ="navbar-brand" href="#">
                        <img src={"/images/cmm.png"} alt="Logo" width="30" height="30" className ="d-inline-block align-text-top"/>
                        atalogo
                    </a>
                </div>
            </nav>
        )
    }
}

export default Navbar;