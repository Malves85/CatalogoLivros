import Select from "./Select";

 
export default function Input(props){

    return(
        <div>
            {props.isBook == true ? (
                <div className="form-group">

                    <label>Isbn </label>
                    <br />
                    <input
                        type="number"
                        className="form-control"
                        name="isbn"
                        onChange={props.onChange}
                        value={props.isbn}
                    />
                    <br />
                    <label>Título </label>
                    <br />
                    <input
                        type="text"
                        className="form-control"
                        name="title"
                        onChange={props.onChange}
                        value={props.title}
                    />
                    <br />

                    <Select
                        value={props.id}
                        onChange={props.onChange}
                    />
                    
                    <label>Preço </label>
                    <input
                        type="number"
                        className="form-control"
                        name="price"
                        onChange={props.onChange}
                        value={props.price}
                    />
                    <br />
                </div>

            ):(
                <div className="form-group">
                    <label>Nome </label>
                    <br />
                    <input
                        type="text"
                        className="form-control"
                        name="name"
                        onChange={props.onChange}
                        value={props.name}
                    />
                    <br />
                    <label>País </label>
                    <br />
                    <input
                        type="text"
                        className="form-control"
                        name="nacionality"
                        onChange={props.onChange}
                        value={props.nacionality}
                    />
                    <br />
                    <label>Imagem (opcional) </label>
                    <br />
                    <input
                        type="text"
                        className="form-control"
                        name="image"
                        onChange={props.onChange}
                        value={props.image}
                    />
                    <br />
                </div>
                
            )}
        </div> 
    );
}