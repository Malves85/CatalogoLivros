import { isPropertySignature } from "typescript";

export default function Search(props){
    return(
        <form className="d-flex" role="search" >
            <input
                style={{ width: "250px", borderRadius: "2px", height: "35px" }}
                className="form-control me-2 bg-light"
                type="search"
                placeholder="Buscar"
                aria-label="Search"
                value={props.value}
                onChange={props.onChange}
            />
            
        </form>
    );
}