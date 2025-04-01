import { Component } from "react";

class Book extends Component{
    state = {
        isFavourite: false,
    }

    toggleFavourite = () => {
        this.setState( {isFavourite: !this.state.isFavourite} );
    }

    render(){
        let btnContent;
        if(!this.state.isFavourite)
            btnContent = "🤍";
        else
            btnContent = "❤️";
        

        return(
            <div className="card" style={{width: "18rem"}}>
                <img src={this.props.image} className="card-img-top" alt="Immagine del libro"/>
                <div className="card-body">
                    <h5 className="card-title text-truncate">{this.props.title}</h5>
                    <p className="card-text text-truncate">{this.props.author}</p>
                    <span>
                        <a href={`https://www.ibs.it/${this.props.link}`} className="btn btn-info">Go to store</a>
                        <button onClick={this.toggleFavourite} type="button" class="btn btn-light">{btnContent}</button>
                    </span>
                </div>
            </div>
        );
    }
}

export default Book; 