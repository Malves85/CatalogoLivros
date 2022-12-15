import "./booksStyle.css";
import { useState, useEffect } from "react";
import axios from "axios";
import "bootstrap/dist/css/bootstrap.min.css";
import { Modal, ModalBody, ModalFooter, ModalHeader } from "reactstrap";
import Card from "react-bootstrap/Card";
import Col from "react-bootstrap/Col";
import Row from "react-bootstrap/Row";
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';

export default function Books() {
  const baseUrl = "https://localhost:7043/api/Books";

  const [data, setData] = useState([]);
  const [modalIncluir, setModalIncluir] = useState(false);
  const [modalEditar, setModalEditar] = useState(false);
  const [modalExcluir, setModalExcluir] = useState(false);
  //filtrar dados
  const [searchInput,setSearchInput]  = useState('');
  const [filtro, setFiltro] = useState([]);

  const [bookSelected, setbookSelected] = useState({
    id: "",
    isbn: "",
    title: "",
    author: "",
    price: "",
  });

  const searchBooks = (searchValue : string) => {
    setSearchInput(searchValue);
    if (searchInput !== '') {
        const dadosFiltrados = data.filter((item) => {
            return Object.values(item).join('').toLowerCase()
            .includes(searchInput.toLowerCase())
        });
        setFiltro(dadosFiltrados);
    }
    else{
        setFiltro(data);
    }
  }



  const selectBook = (book: any, opcao: string) => {
    setbookSelected(book);
    opcao === "Editar" ? abrirFecharModalEditar() : abrirFecharModalExcluir();
  };

  const abrirFecharModalIncluir = () => {
    setModalIncluir(!modalIncluir);
  };
  const abrirFecharModalEditar = () => {
    setModalEditar(!modalEditar);
  };
  const abrirFecharModalExcluir = () => {
    setModalExcluir(!modalExcluir);
  };

  const handleChange = (e: any) => {
    const { name, value } = e.target;
    setbookSelected({
      ...bookSelected,
      [name]: value,
    });
    console.log(bookSelected);
  };

  const pedidoGet = async () => {
    await axios
      .get(baseUrl)
      .then((response) => {
        setData(response.data);
      })
      .catch((error) => {
        console.log(error);
      });
  };

  const pedidoPost = async () => {
    delete bookSelected.id;
    await axios
      .post(baseUrl, bookSelected)
      .then((response) => {
        setData(data.concat(response.data));
        abrirFecharModalIncluir();
      })
      .catch((error) => {
        console.log(error);
      });
  };

  const pedidoPut = async () => {
    await axios
      .put(baseUrl + "/" + bookSelected.id, bookSelected)
      .then((response) => {
        var resposta = response.data;
        var dadosAuxiliar = data;
        dadosAuxiliar.map(
          (book: {
            id: string;
            isbn: number;
            title: string;
            author: string;
            price: number;
          }) => {
            if (book.id === bookSelected.id) {
              book.isbn = resposta.isbn;
              book.title = resposta.title;
              book.author = resposta.author;
              book.price = resposta.price;
            }
          }
        );
        abrirFecharModalEditar();
      })
      .catch((error) => {
        console.log(error);
      });
  };

  const pedidoDelete = async () => {
    await axios
      .delete(baseUrl + "/" + bookSelected.id)
      .then((response) => {
        setData(data.filter((book) => book !== response.data));
        abrirFecharModalExcluir();
      })
      .catch((error) => {
        console.log(error);
      });
  };

  useEffect(() => {
    pedidoGet();
  });
  return (
    <div className="Book-container">

        <form className="d-flex" role="search">
            <input
            className="form-control me-2 bg-light"
            type="search"
            placeholder="Search by title"
            aria-label="Search"
            onChange={(e) => searchBooks(e.target.value)}
            />
        </form>
        <button
          className="btn btn-success md-2"
          onClick={() => abrirFecharModalIncluir()}
        >
          Incluir novo Livro
        </button>

      {searchInput.length > 1 ? (
        <Row xs={2|1} md={3} className="g-1">
        {filtro.map(
          (book: {
            id: number;
            isbn: number;
            title: string;
            author: string;
            price: number;
          }) => (
            <Col>
              <Card border="primary" bg="light">
                <Card.Body>
                  <Card.Title>Book</Card.Title>
                  <Card.Text>
                    <p key={book.id}>
                      <b>Isbn </b>
                      {book.isbn}
                      <br></br>
                      <b>Title </b>
                      {book.title}
                      <br></br>
                      <b>Author </b>
                      {book.author}
                      <br></br>
                      <b>Price </b>
                      {book.price}
                      <br></br>
                      <button
                        className="btn btn-primary"
                        onClick={() => selectBook(book, "Editar")}
                      >
                        Editar
                      </button>{" "}
                      <button
                        className="btn btn-danger"
                        onClick={() => selectBook(book, "Excluir")}
                      >
                        Excluir
                      </button>
                    </p>
                  </Card.Text>
                </Card.Body>
              </Card>
            </Col>
          )
        )}
      </Row>
               
              ) : (
                <Row xs={2|1} md={3} className="g-1">
        {data.map(
          (book: {
            id: number;
            isbn: number;
            title: string;
            author: string;
            price: number;
          }) => (
            <Col>
              <Card border="primary" bg="light">
                <Card.Body>
                  <Card.Title>Book</Card.Title>
                  <Card.Text>
                    <p key={book.id}>
                      <b>Isbn </b>
                      {book.isbn}
                      <br></br>
                      <b>Title </b>
                      {book.title}
                      <br></br>
                      <b>Author </b>
                      {book.author}
                      <br></br>
                      <b>Price </b>
                      {book.price}
                      <br></br>
                      <button
                        className="btn btn-primary"
                        onClick={() => selectBook(book, "Editar")}
                      >
                        Editar
                      </button>{" "}
                      <button
                        className="btn btn-danger"
                        onClick={() => selectBook(book, "Excluir")}
                      >
                        Excluir
                      </button>
                    </p>
                  </Card.Text>
                </Card.Body>
              </Card>
            </Col>
          )
        )}
      </Row>
            
           )}


      {/* 
      <table className="table table-bordered">
        <thead>
          <tr>
            <th scope="col">Id</th>
            <th scope="col">Isbn</th>
            <th scope="col">Title</th>
            <th scope="col">Author</th>
            <th scope="col">Price</th>
            <th scope="col">Operação</th>
          </tr>
        </thead>
        <tbody>
          {data.map(
            (book: {
              id: number;
              isbn: number;
              title: string;
              author: string;
              price: number;
            }) => (
              <tr key={book.id}>
                <td>{book.id}</td>
                <td>{book.isbn}</td>
                <td>{book.title}</td>
                <td>{book.author}</td>
                <td>{book.price}</td>
                <td>
                  <button
                    className="btn btn-primary"
                    onClick={() => selectBook(book, "Editar")}
                  >
                    Editar
                  </button>{" "}
                  <button
                    className="btn btn-danger"
                    onClick={() => selectBook(book, "Excluir")}
                  >
                    Excluir
                  </button>
                </td>
              </tr>
            )
          )}
        </tbody>
      </table>
       */}

      {/* Modal incluir alunos */}
      <Modal isOpen={modalIncluir}>
        <ModalHeader>Incluir novo Livro</ModalHeader>
        <ModalBody>
          <div className="form-group">
            <label>Isbn: </label>
            <br />
            <input
              type="text"
              className="form-control"
              name="isbn"
              onChange={handleChange}
            />
            <br />
            <label>Title: </label>
            <br />
            <input
              type="text"
              className="form-control"
              name="title"
              onChange={handleChange}
            />
            <br />
            <label>Author: </label>
            <input
              type="text"
              className="form-control"
              name="author"
              onChange={handleChange}
            />
            <br />
            <label>price: </label>
            <input
              type="number"
              className="form-control"
              name="price"
              onChange={handleChange}
            />
            <br />
          </div>
        </ModalBody>
        <ModalFooter>
          <button className="btn btn-primary" onClick={() => pedidoPost()}>
            Incluir
          </button>{" "}
          {"   "}
          <button
            className="btn btn-danger"
            onClick={() => abrirFecharModalIncluir()}
          >
            Cancelar
          </button>
        </ModalFooter>
      </Modal>

      {/* Modal Editar Alunos */}
      <Modal isOpen={modalEditar}>
        <ModalHeader>Editar Aluno</ModalHeader>
        <ModalBody>
          <div className="form-group">
            <label>Id: </label>
            <input
              type="text"
              className="form-control"
              readOnly
              value={bookSelected && bookSelected.id}
            />
            <br />
            <label>Isbn: </label>
            <br />
            <input
              type="text"
              className="form-control"
              name="isbn"
              onChange={handleChange}
              value={bookSelected && bookSelected.isbn}
            />
            <br />
            <label>Title: </label>
            <br />
            <input
              type="text"
              className="form-control"
              name="title"
              onChange={handleChange}
              value={bookSelected && bookSelected.title}
            />
            <br />
            <label>Author: </label>
            <input
              type="text"
              className="form-control"
              name="author"
              onChange={handleChange}
              value={bookSelected && bookSelected.author}
            />
            <br />
            <label>Price: </label>
            <input
              type="number"
              className="form-control"
              name="price"
              onChange={handleChange}
              value={bookSelected && bookSelected.price}
            />
            <br />
          </div>
        </ModalBody>
        <ModalFooter>
          <button className="btn btn-primary" onClick={() => pedidoPut()}>
            Editar
          </button>{" "}
          {"   "}
          <button
            className="btn btn-danger"
            onClick={() => abrirFecharModalEditar()}
          >
            Cancelar
          </button>
        </ModalFooter>
      </Modal>

      {/* Modal excluir alunos */}
      <Modal isOpen={modalExcluir}>
        <ModalBody>
          Confirma a exclusão do livro {bookSelected && bookSelected.title} ?
        </ModalBody>
        <ModalFooter>
          <button className="btn btn-danger" onClick={() => pedidoDelete()}>
            Sim
          </button>
          <button
            className="btn btn-secondary"
            onClick={() => abrirFecharModalExcluir()}
          >
            Não
          </button>
        </ModalFooter>
      </Modal>
    </div>
  );
}