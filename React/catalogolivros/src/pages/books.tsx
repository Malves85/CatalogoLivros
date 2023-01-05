import "./booksStyle.css";
import { useState, useEffect } from "react";
import axios from "axios";
import "bootstrap/dist/css/bootstrap.min.css";
import { Modal, ModalBody, ModalFooter, ModalHeader } from "reactstrap";
import Card from "react-bootstrap/Card";
import Col from "react-bootstrap/Col";
import Row from "react-bootstrap/Row";
import ReactPaginate from "react-paginate";
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import Toast from "../Helpers/toast";

export default function Books() {

  const baseUrl = "https://localhost:7043/api/Books";
  const [data, setData] = useState([]);
  const [updateData, setUpdateData] = useState(true);
  const [modalIncluir, setModalIncluir] = useState(false);
  const [modalEditar, setModalEditar] = useState(false);
  const [modalExcluir, setModalExcluir] = useState(false);
  //filtrar dados
  const [searchInput, setSearchInput] = useState("");
  //paginação
  const [pageCount, setPageCount] = useState(1);
  const pageSize = 6;
  const [curPage, setCurPage] = useState(1);
  //ordenar
  const [sortValue, setSortValue] = useState("");
  const sortOptions = ["Isbn", "Title", "Author", "Price"];
  const [forcePage, setForcePage] = useState(0);

  const [bookSelected, setbookSelected] = useState({
    id: "",
    isbn: 0,
    title: "",
    author: "",
    price: 0,
  });

  const [getBooks, setGetBooks] = useState({
  currentPage: 1,
  pageSize: 6,
  searching: "",
  sorting: "",
  });

  const selectBook = (book: any, opcao: string) => {
    setbookSelected(book);
    opcao === "Editar" ? abrirFecharModalEditar() : abrirFecharModalExcluir();
  };

  const abrirFecharModalIncluir = () => {
    
    setbookSelected ({ ...bookSelected, 
      isbn: 0,
      title: "",
      author: "",
      price: 0
    })
    setModalIncluir(!modalIncluir);
  };
  const abrirFecharModalEditar = () => {
      
    setModalEditar(!modalEditar);
  };
  const abrirFecharModalExcluir = () => {
    setModalExcluir(!modalExcluir);
  };

  //Filtro

  const searchReset = async () => {
    setSearchInput("");
    setGetBooks({
      ...getBooks
    })
  };

  const searchBooks = async (e: any) => {
    e.preventDefault();
    setGetBooks({
      ...getBooks, searching:searchInput, currentPage : 1
    })
    setForcePage(0);
    setUpdateData(true);
  };
      
  const sortBooks = async (e: any) => {
    let value =  e.target.value;
    setSortValue(value);
    setGetBooks({
      ...getBooks, sorting:value
    }) 
    setUpdateData(true);
  };
  //end
  
  //recebe os dados inserido nos formularios incluir ou editar livro
  const handleChange = (e: any) => {
    const { name, value } = e.target;
    setbookSelected({
      ...bookSelected,
      [name]: value,
    });
    
  };
  //end
  
  //Busca todos os dados com a paginação
  const pedidoGet = async () => {
    const res = await (await axios.post(`${baseUrl}/getAll`,getBooks)).data;
    const total:number = res.totalRecords;
    setPageCount(res.totalPages);
    console.log("total "+total);

    await axios
      .post(`${baseUrl}/getAll`,getBooks)
      .then((response) => {
        setData(response.data.items);
        console.log("current "+res.currentPage)
      })
      .catch((error) => {
        console.log(error);
      });
  };
  //end
  

  //Envia os dados do novo livro
  const pedidoPost = async () => {
    delete bookSelected.id;
    await axios
      .post(baseUrl+"/create", bookSelected)
      .then((response) => {
        setData(data.concat(response.data));
        if (response.data.success)
        {
          Toast.Show("success", response.data.message);
          abrirFecharModalIncluir();
        }else{
          Toast.Show("error", response.data.message);
        }
        setUpdateData(true);
        
      })
      .catch((error) => {
        console.log(error);
      });
  };
  //End

  //Envia os dados da edição de um livro
  const pedidoPut = async () => {
    await axios
      .post(baseUrl + "/update" , bookSelected)
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
            
            if (book.id === bookSelected.id){
              book.isbn = resposta.isbn;
              book.title = resposta.title;
              book.author = resposta.author;
              book.price = resposta.price;
            }  
          }
        );

        if (response.data.success)
        {Toast.Show("success", response.data.message);
        }
        else{
          Toast.Show("error", response.data.message);
        }
        abrirFecharModalEditar();
        setUpdateData(true);
        
      })
      .catch((error) => {
        console.log(error);
      });
  };
  //end

  //Altera a visibilidade do livro deletado, ou seja soft delete
  const pedidoDelete = async () => {
    await axios
      .delete(baseUrl + "/" + bookSelected.id)
      .then((response) => {
        setData(data.filter((book) => book !== response.data));
        setUpdateData(true);
        abrirFecharModalExcluir();
        if(response.data.success){
          Toast.Show("success", response.data.message);
        }
        else{
          Toast.Show("error", response.data.message);
        }
      })
      .catch((error) => {
        console.log(error);
      });
  };
  //end

  //Atualiza os dados da pagina
  useEffect(() => {
    if (updateData) {
      pedidoGet();
      setUpdateData(false);
    }
  },[updateData]);
  //end

  // Paginação
  /* const fetchBooks = async (currentPage: number) => {
    //if(searchInput === "" && sortValue === ""){
    const res = await fetch(`${baseUrl}/getAll`, getBooks);
    console.log("res "+res)
    const temp = res.currentPage;
    console.log("temp "+temp)
    //setData(await axios.post(`${baseUrl}/getAll`,{ ...getBooks, currentPage: current}));
    return temp;
    /*}else if(searchInput !== "") {
      const res = await fetch(`${baseUrl}?currentPage=${currentPage}&search=${searchInput}`);
    const temp = res.json();
    return temp;
    }
    else{
      const res = await fetch(`${baseUrl}?currentPage=${currentPage}&order=${sortValue}`);
    const temp = res.json();
    return temp;
    }
    
  };
console.log("data "+data);*/

  const handlePageClick = async (data:any)=>{
   /* console.log(data.selected+1);
    let currentPage = data.selected+1;
    const booksFormServer = await fetchBooks(currentPage);
    setData (booksFormServer.items);
    setUpdateData(true);
    console.log("bookFormServer "+booksFormServer);*/
    let current = data.selected+1;
    setGetBooks({
      ...getBooks, currentPage:current
    })
    setForcePage(data.selected);
    setUpdateData(true);
  }
  // end
  
  return (
    <div className="Book-container">
      <form className="d-flex" role="search" onSubmit={searchBooks}>
        <input
          className="form-control me-2 bg-light"
          type="search"
          placeholder="Buscar"
          aria-label="Search"
          value={searchInput}
          onChange={(e) => setSearchInput(e.target.value)}
        />
        <button className="btn btn-success md-2" type="submit">
        Ok</button>
        <button className="btn btn-danger md-2" onClick={() => searchReset()}>
        Resetar</button>
      </form>
      
      <Row >
        <Col>
        <button className="btn btn-success md-2" onClick={() => abrirFecharModalIncluir()}
      >Incluir novo Livro</button>

      <ToastContainer
      position="top-center"
      autoClose={5000}
      hideProgressBar={false}
      newestOnTop={false}
      closeOnClick
      rtl={false}
      pauseOnFocusLoss
      draggable
      pauseOnHover
      theme="light"
      />

        </Col>
        <Col>
        <Row>
          <Col>
          <h5>Ordenar por:</h5>
          </Col>
          <Col>
            <select style={{width:"50&", borderRadius:"2px", height:"35px"}}
            onChange={sortBooks}
            value={sortValue}>
              <option>Id</option>
              {sortOptions.map((item, index) => (
                <option value={item} key={index}>
                  {item}
                </option>
              ))}
            </select>
          </Col>
                <Col></Col><Col></Col>
                
        </Row>
        </Col>
      </Row>

      {
        data.length === 0 && searchInput.length > 2 ? (
        <Row xs={2 | 1} md={3} className="g-1" >
          <div className="justify-content-center">
            <h4>Livro não encontrado</h4>
          </div>
        </Row>
      ) : (
        <Row xs={2 | 1} md={3} className="g-1">
          {data.map(
            (book: {
              id: number;
              isbn: number;
              title: string;
              author: string;
              price: any;
            }) => (
              <Col key={book.id}>
                <Card border="primary" bg="light">
                  <Card.Body>
                    <Card.Title>Livro</Card.Title>
                    <Card.Text>
                      
                        <b>Isbn </b>
                        {book.isbn}
                        <br></br>
                        <b>Título </b>
                        {book.title}
                        <br></br>
                        <b>Autor </b>
                        {book.author}
                        <br></br>
                        <b>Preço </b>
                        {parseFloat(book.price).toFixed(2).toString().replace(".",",")}€
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
                      
                    </Card.Text>
                  </Card.Body>
                </Card>
              </Col>
            )
          )}
        </Row>
      )
      }
      

<ReactPaginate 
        previousLabel={'previous'}
        nextLabel={'next'}
        breakLabel={'...'} 
        forcePage={forcePage}
        pageCount={pageCount}
        marginPagesDisplayed={2}
        pageRangeDisplayed={3}
        onPageChange={handlePageClick}
        containerClassName={'pagination justify-content-center'}
        pageClassName={'page-item'}
        pageLinkClassName={'page-link'}
        previousClassName={'page-item'}
        previousLinkClassName={'page-link'}
        nextClassName={'page-item'}
        nextLinkClassName={'page-link'}
        breakClassName={'page-item'}
        breakLinkClassName={'page-link'}
        activeClassName={'active'}
  />

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
            <label>Título: </label>
            <br />
            <input
              type="text"
              className="form-control"
              name="title"
              onChange={handleChange}
            />
            <br />
            <label>Autor: </label>
            <input
              type="text"
              className="form-control"
              name="author"
              onChange={handleChange}
            />
            <br />
            <label>Preço: </label>
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
            <label>Título: </label>
            <br />
            <input
              type="text"
              className="form-control"
              name="title"
              onChange={handleChange}
              value={bookSelected && bookSelected.title}
            />
            <br />
            <label>Autor: </label>
            <input
              type="text"
              className="form-control"
              name="author"
              onChange={handleChange}
              value={bookSelected && bookSelected.author}
            />
            <br />
            <label>Preço: </label>
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
