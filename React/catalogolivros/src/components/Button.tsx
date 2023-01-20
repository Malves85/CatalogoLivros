import { Button } from "reactstrap";

export default function Buttons(props){
    return(
        <div>
            <Button onClick={props.onClick}>
                {props.label}
            </Button>{" "}
        </div>
    );
}