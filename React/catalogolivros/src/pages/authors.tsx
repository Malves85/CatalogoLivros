import "./books.css";
import { useState, useEffect } from "react";
import axios from "axios";
import "bootstrap/dist/css/bootstrap.min.css";
import { CardImg, Modal, ModalBody, ModalFooter, ModalHeader } from "reactstrap";
import Card from "react-bootstrap/Card";
import Col from "react-bootstrap/Col";
import Row from "react-bootstrap/Row";
import ReactPaginate from "react-paginate";
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import Toast from "../Helpers/toast";

export default function Authors(){

    const baseUrl = "https://localhost:7043/api/Authors";
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
  const sortOptions = ["Nome", "País"];
  const [forcePage, setForcePage] = useState(0);

  const [authorSelected, setAuthorSelected] = useState({
    id: "",
    name: "",
    nacionality: "",
    image: ""
  });

  const [getAuthors, setGetAuthors] = useState({
  currentPage: 1,
  pageSize: 6,
  searching: "",
  sorting: "",
  });

  const selectAuthor = (author: any, opcao: string) => {
    setAuthorSelected(author);
    opcao === "Editar" ? abrirFecharModalEditar() : abrirFecharModalExcluir();
  };

  const abrirFecharModalIncluir = () => {
    
    setAuthorSelected ({ ...authorSelected, 
      name: "", nacionality: "", image:""
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
    setGetAuthors({
      ...getAuthors
    })
  };

  const searchAuthors = async (e: any) => {
    e.preventDefault();
    setGetAuthors({
      ...getAuthors, searching:searchInput, currentPage : 1
    })
    setForcePage(0);
    setUpdateData(true);
  };
      
  const sortAuthors = async (e: any) => {
    let value =  e.target.value;
    setSortValue(value);
    setGetAuthors({
      ...getAuthors, sorting:value
    }) 
    setUpdateData(true);
  };
  //end
  
  //recebe os dados inserido nos formularios incluir ou editar livro
  const handleChange = (e: any) => {
    const { name, value } = e.target;
    setAuthorSelected({
      ...authorSelected,
      [name]: value,
    });
    
  };
  //end
  
  //Busca todos os dados com a paginação
  const pedidoGet = async () => {
    const res = await (await axios.post(`${baseUrl}/getAuthors`,getAuthors)).data;
    const total:number = res.totalRecords;
    setPageCount(res.totalPages);
    console.log("total "+total);

    await axios
      .post(`${baseUrl}/getAuthors`,getAuthors)
      .then((response) => {
        setData(response.data.items);
      })
      .catch((error) => {
        console.log(error);
      });
  };
  //end
  

  //Envia os dados do novo livro
  const pedidoPost = async () => {
    delete authorSelected.id;
    await axios
      .post(baseUrl+"/create", authorSelected)
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
      .post(baseUrl + "/update" , authorSelected)
      .then((response) => {
        var resposta = response.data;
        var dadosAuxiliar = data;
        dadosAuxiliar.map(
          (author: {
            id: string;
            name: string;
            nacionality: string;
            image: string;
          }) => {
            
            if (author.id === authorSelected.id){
                author.name = resposta.name;
                author.nacionality = resposta.nacionality;
                author.image = resposta.image;                
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
      .delete(baseUrl + "/" + authorSelected.id)
      .then((response) => {
        setData(data.filter((author) => author !== response.data));
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

  const handlePageClick = async (data:any)=>{
    let current = data.selected+1;
    setGetAuthors({
      ...getAuthors, currentPage:current
    })
    setForcePage(data.selected);
    setUpdateData(true);
  }
  // end
  
  return (
    <div className="Book-container">
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
      <br></br>
      <Row >
      <Col>
        <button className="btn btn-success md-2" onClick={() => abrirFecharModalIncluir()}
      >Incluir novo Autor</button>
        <br></br>
        </Col>
        <Col></Col>
        <Col>    
        <h5>Ordenar por:</h5>
        </Col>
        <Col>
          <select style={{width:"90px", borderRadius:"2px", height:"35px"}}
            onChange={sortAuthors}
            value={sortValue}>
              <option>Id</option>
              {sortOptions.map((item, index) => (
                <option value={item} key={index}>
                  {item}
                </option>
              ))}
            </select>
          </Col>
          <Col></Col>
        <Col>
        <form className="d-flex" role="search" onSubmit={searchAuthors}>
        <input style={{width:"250px", borderRadius:"2px", height:"35px"}}
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
        </Col>
        <br></br><br></br>
        
      </Row>

      {
        data.length === 0 && searchInput.length > 2 ? (
        <Row xs={2 | 1} md={3} className="g-1" >
          <div className="justify-content-center">
            <h4>Autor não encontrado</h4>
          </div>
        </Row>
      ) : (
        <Row xs={2 | 1} md={3} className="g-1">
          {data.map(
            (author: {
              id: number;
              name: string;
              nacionality: string;
              image: string;
            }) => (
              <Col key={author.id}  width="50px">
                <Card border="primary" bg="light">
                  <Card.Body>
                    <Card.Title>{author.name}</Card.Title>
                    <Card.Text>
                        <br></br>
                        <CardImg width="50%" src={author.image} alt={author.name} />
                        <br></br>
                        <b>País: </b>
                        {author.nacionality}
                        <br></br><br></br>
                        <button
                          className="btn btn-primary"
                          onClick={() => selectAuthor(author, "Editar")}
                        >
                          Editar
                        </button>{" "}
                        <button
                          className="btn btn-danger"
                          onClick={() => selectAuthor(author, "Excluir")}
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

      {/* Modal incluir autor */}
      <Modal isOpen={modalIncluir}>
        <ModalHeader>Incluir novo Autor</ModalHeader>
        <ModalBody>
          <div className="form-group">
            <label>Name: </label>
            <br />
            <input
              type="text"
              className="form-control"
              name="name"
              onChange={handleChange}
            />
            <br />
            <label>Nacionality: </label>
            <br />
            <input
              type="text"
              className="form-control"
              name="nacionality"
              onChange={handleChange}
            />
            <br />
            <label>Imagem: </label>
            <br />
            <input
              type="text"
              className="form-control"
              name="image"
              onChange={handleChange}
            />
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

      {/* Modal Editar Autores */}
      <Modal isOpen={modalEditar}>
        <ModalHeader>Editar Autor</ModalHeader>
        <ModalBody>
          <div className="form-group">
            <label>Id: </label>
            <input
              type="text"
              className="form-control"
              readOnly
              value={authorSelected && authorSelected.id}
            />
            <br />
            <label>Name: </label>
            <br />
            <input
              type="text"
              className="form-control"
              name="name"
              onChange={handleChange}
              value={authorSelected && authorSelected.name}
            />
            <br />
            <label>Nacionality: </label>
            <br />
            <input
              type="text"
              className="form-control"
              name="nacionality"
              onChange={handleChange}
              value={authorSelected && authorSelected.nacionality}
            />
            <br />
            <label>Imagem: </label>
            <br />
            <input
              type="text"
              className="form-control"
              name="image"
              onChange={handleChange}
              value={authorSelected && authorSelected.image}
            />
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

      {/* Modal excluir autores */}
      <Modal isOpen={modalExcluir}>
        <ModalBody>
          Confirma a exclusão do autor {authorSelected && authorSelected.name} ?
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