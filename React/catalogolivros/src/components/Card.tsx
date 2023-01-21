import "bootstrap/dist/css/bootstrap.min.css";
import Card from "react-bootstrap/Card";
export default function Cards(props) {
  return (
    
    <Card.Body>
        {props.isBook == true ? (
          <div>
            <Card.Title>{props.title}</Card.Title>
            <br />
            <Card.Img
              variant="top"
              style={{ width: "200px", borderRadius: "5px", height: "150px" }}
              src={props.image}
            />
            <Card.Text>
              <b>Isbn: </b>
              {props.isbn}
              <br></br>
              <b>Autor: </b>
              {props.authorName}
              <br></br>
              <b>Preço: </b>
              {parseFloat(props.price).toFixed(2).toString().replace(".", ",")}€
              <br></br>
            </Card.Text>
          </div>
        ) : (
          <div>
            <Card.Title>{props.name}</Card.Title>
            <br />
            <Card.Img
              variant="top"
              style={{ width: "200px", borderRadius: "5px", height: "150px" }}
              src={props.image}
            />
            <Card.Text>
              <br></br>
              <b>País: </b>
              {props.nacionality}
              <br></br>
              <b>Livros: </b>
              {props.authorTitle}
              <br></br>
            </Card.Text>
          </div>
        )}
      </Card.Body>
  );
}
