export default function OrderBy(props){
    return(
        <select
            style={{ width: "90px", borderRadius: "5px", height: "35px" }}
            onChange={props.onChange}
            value={props.value}>
                <option>Id</option>
                {props.map}
        </select>
    );
}