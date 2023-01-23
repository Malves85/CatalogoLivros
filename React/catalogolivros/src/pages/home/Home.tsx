import React from "react";
import '../../styles/Home.css';
import Card from "react-bootstrap/Card";

export default function Home(){
    return(
        <div>
        <h3>Catalogo de livros</h3>
        <Card.Img

              variant="top"
              style={{ width: "1000px", borderRadius: "5px", height: "500px"}}
              src={"https://cdn.pixabay.com/photo/2016/01/19/01/42/library-1147815_960_720.jpg"}
            />
        </div>
    );
}