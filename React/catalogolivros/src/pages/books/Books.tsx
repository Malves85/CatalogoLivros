import "../../styles/Books.css";
import { useState, useEffect } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import { Button, Card} from "reactstrap";
import CardBody  from "../../components/Card"
import Col from "react-bootstrap/Col";
import Row from "react-bootstrap/Row";
import ReactPaginate from "react-paginate";
import "react-toastify/dist/ReactToastify.css";
import { useNavigate } from "react-router-dom";
import Toast from "../../helpers/Toast";
import { BookService } from "../../services/BookService";
import Search from "../../components/Search";
import OrderBy from "../../components/OrderBy";

export default function Books() {
    const [updateData, setUpdateData] = useState(true);
    const [searchInput, setSearchInput] = useState("");
    const [pageCount, setPageCount] = useState(1);
    const [sortValue, setSortValue] = useState("");
    const sortOptions = ["Autor", "Isbn", "Preço", "Título"];
    const [forcePage, setForcePage] = useState(0);
    const navigate = useNavigate();
    const [currentPage, setCurrentPage] = useState(0);
    const [pageSize, setPageSize] = useState(6);

    const [books, setBooks] = useState([]);
    const bookService = new BookService();
    
    const loadBooks = async () => {
        var response = await bookService.GetAll(currentPage, pageSize, searchInput, sortValue);
        
        if (response.success !== true) {
            Toast.Show("error", "Erro ao carregar os livros!");
            return;
        }

        if (response.items == null) {
            Toast.Show("error", "Não existem livros!");
            return;
        }
        setBooks(response.items);
        setPageCount(response.totalPages);
        setUpdateData(true);
    };
    


    //Atualiza os dados da pagina
    useEffect(() => {
        if (updateData) {
            loadBooks();
            setUpdateData(false);
        }
    }, [updateData]);
    //end

    const handlePageClick = async (data: any) => {
        let current = data.selected + 1;
        setCurrentPage(current);
        setForcePage(data.selected);
        
    };
    // end

    return (
        <div className="Book-container">
        
        <br></br>
        <Row>
            <Col>
            <Button style={{ backgroundColor:"darkgreen" }}
            onClick={() =>  navigate(`/createBook`)}>
                Incluir novo Livro
            </Button>
            <br></br>
            </Col>
            <Col></Col>
            <Col>
                <h5>Ordenar por:</h5>
            </Col>
            <Col>
                <OrderBy
                    onChange={(e) => (setSortValue(e.target.value),setCurrentPage(0),setForcePage(0))}
                    value={sortValue}
                    map={sortOptions.map((item, index) => (<option value={item} key={index}> {item} </option>))}
                />
            </Col>
            <Col></Col>
            <Col>
                <Search
                    value={searchInput}
                    onChange={(e) => (setSearchInput(e.target.value),setCurrentPage(0),setForcePage(0))}
                />    
            </Col>
            <br></br>
            <br></br>
        </Row>

        {books.length === 0 && searchInput.length > 2 ? (
            <Row xs={2 | 1} md={3} className="g-1">
            <div className="justify-content-center">
                <h4>Livro não encontrado</h4>
            </div>
            </Row>
        ) : (
            <Row xs={2 | 1} md={3} className="g-3">
            {books.map(
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
                    isBook={true}
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