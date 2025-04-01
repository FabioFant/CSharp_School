import { useState } from 'react';

function Quiz(props){
    const[isCorrect, setCorrect] = useState(true);

    return(
        <div className="card">
            <div className="card-body">
                <h5 className="card-title">{props.question}</h5>
                <div className={props.question} role="group" aria-label="Basic radio toggle button group">
                    <input type="radio" className="btn-check" name="btnradio" id={props.options[0]} autocomplete="off"/>
                    <label className="btn btn-outline-primary" for={props.options[0]}>{props.options[0]}</label>

                    <input type="radio" className="btn-check" name="btnradio" id={props.options[1]} autocomplete="off"/>
                    <label className="btn btn-outline-primary" for={props.options[1]}>{props.options[1]}</label>

                    <input type="radio" className="btn-check" name="btnradio" id={props.options[2]} autocomplete="off"/>
                    <label className="btn btn-outline-primary" for={props.options[2]}>{props.options[2]}</label>

                    <input type="radio" className="btn-check" name="btnradio" id={props.options[3]} autocomplete="off"/>
                    <label className="btn btn-outline-primary" for={props.options[3]}>{props.options[3]}</label>
                </div>
            </div>
        </div>
    )
}

export default Quiz;