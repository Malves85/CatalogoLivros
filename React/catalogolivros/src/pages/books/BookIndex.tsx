import "../../styles/Books.css";
import { useState, useEffect } from "react";
import axios from "axios";
import "bootstrap/dist/css/bootstrap.min.css";
import { Button, Card} from "reactstrap";
import CardBody  from "../../components/Card"
import Col from "react-bootstrap/Col";
import Row from "react-bootstrap/Row";
import ReactPaginate from "react-paginate";
import "react-toastify/dist/ReactToastify.css";
import Toast from "../../helpers/Toast";
import { useNavigate } from "react-router-dom";

export default function BookIndex() {
  const baseUrl = "https://localhost:7043/api/Books";
  const [data, setData] = useState([]);
  const [updateData, setUpdateData] = useState(true);
  const [searchInput, setSearchInput] = useState("");
  const [pageCount, setPageCount] = useState(1);
  const [sortValue, setSortValue] = useState("");
  const sortOptions = ["Autor", "Isbn", "Preço", "Título"];
  const [forcePage, setForcePage] = useState(0);
  const navigate = useNavigate();

  const [bookSelected, setbookSelected] = useState({
    id: "",
    isbn: 0,
    title: "",
    authorId: 0,
    price: 0,
  });
  

  const [getBooks, setGetBooks] = useState({
    currentPage: 1,
    pageSize: 6,
    searching: "",
    sorting: "",
  });

  //Filtro

  const searchReset = async () => {
    setSearchInput("");
    setGetBooks({
      ...getBooks,
    });
  };

  const searchBooks = async (e: any) => {
    e.preventDefault();
    setGetBooks({
      ...getBooks,
      searching: searchInput,
      currentPage: 1,
    });
    setForcePage(0);
    setUpdateData(true);
  };

  const sortBooks = async (e: any) => {
    let value = e.target.value;
    setSortValue(value);
    setGetBooks({
      ...getBooks,
      sorting: value,
    });
    setUpdateData(true);
  };
  
  //end

  //Busca todos os dados com a paginação
  const pedidoGet = async () => {
    const res = await (await axios.post(`${baseUrl}/getAll`, getBooks)).data;
    const total: number = res.totalRecords;
    setPageCount(res.totalPages);
    console.log("total " + total);

    await axios
      .post(`${baseUrl}/getAll`, getBooks)
      .then((response) => {
        setData(response.data.items);
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
  }, [updateData]);
  //end

  const handlePageClick = async (data: any) => {
    let current = data.selected + 1;
    setGetBooks({
      ...getBooks,
      currentPage: current,
    });
    setForcePage(data.selected);
    setUpdateData(true);
  };

  return (
    <div className="Book-container">
      
      <br></br>
      <Row>
        <Col>
          <Button style={{ backgroundColor:"darkgreen" }}href="/createBook">
            Incluir novo Livro
          </Button>
          <br></br>
        </Col>
        <Col></Col>
        <Col>
          <h5>Ordenar por:</h5>
        </Col>
        <Col>
          <select
            style={{ width: "120px", borderRadius: "4px", height: "35px" }}
            onChange={sortBooks}
            value={sortValue}
          >
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
          <form className="d-flex" role="search" onSubmit={searchBooks}>
            <input
              style={{ width: "250px", borderRadius: "2px", height: "35px" }}
              className="form-control me-2 bg-light"
              type="search"
              placeholder="Buscar"
              aria-label="Search"
              value={searchInput}
              onChange={(e) => setSearchInput(e.target.value)}
            />
            <Button  style={{ backgroundColor:"blue" }}type="submit">
              Ok
            </Button>
            <Button  style={{ backgroundColor:"red" }}
              onClick={() => searchReset()}
            >
              Resetar
            </Button>
          </form>
        </Col>
        <br></br>
        <br></br>
      </Row>

      {data.length === 0 && searchInput.length > 2 ? (
        <Row xs={2 | 1} md={3} className="g-1">
          <div className="justify-content-center">
            <h4>Livro não encontrado</h4>
          </div>
        </Row>
      ) : (
        <Row xs={2 | 1} md={3} className="g-3">
          {data.map(
            (book: {
              id: number;
              isbn: number;
              title: string;
              authorName: string;
              price: string;
              image : string;
            }) => (
              <Col key={book.id}>
                <Card border="primary" bg="light" className="text-center" style={{ width: '27rem'}}>
                <CardBody
                isbn={book.isbn}
                title={book.title}
                authorName={book.authorName}
                price={book.price}
                image={book.image}/>
                <Row>
                    <Col/>
                    
                      <Button
                        style={{ width: "100px", height: "35px", backgroundColor: "blue" }}
                        onClick={() => ( navigate(`/editBook/${book.id}`))}
                      >
                        Detalhes
                      </Button>
                      <br /><br />
                    <Col/>
                  </Row>
                </Card>
              </Col>
              
            )
          )}
        </Row>
        
      )}
      <br/>
      <ReactPaginate
        previousLabel={"previous"}
        nextLabel={"next"}
        breakLabel={"..."}
        forcePage={forcePage}
        pageCount={pageCount}
        marginPagesDisplayed={2}
        pageRangeDisplayed={3}
        onPageChange={handlePageClick}
        containerClassName={"pagination justify-content-center"}
        pageClassName={"page-item"}
        pageLinkClassName={"page-link"}
        previousClassName={"page-item"}
        previousLinkClassName={"page-link"}
        nextClassName={"page-item"}
        nextLinkClassName={"page-link"}
        breakClassName={"page-item"}
        breakLinkClassName={"page-link"}
        activeClassName={"active"}
      />
    </div>
  );
}
