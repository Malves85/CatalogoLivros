import Select from "./Select";

 
export default function Input(props){

    return(

        <div className="form-group">
            <label>Isbn </label>
            <br />
            <input
              type="text"
              className="form-control"
              name="isbn"
              onChange={props.onChange}
            />
            <br />
            <label>Título: </label>
            <br />
            <input
              type="text"
              className="form-control"
              name="title"
              onChange={props.onChange}
            />
            <br />
            <Select
                onChange={props.onChange}
            />
            <br />
            <label>Preço: </label>
            <input
              type="number"
              className="form-control"
              name="price"
              onChange={props.onChange}
            />
            <br />
        </div>
        
    );
}