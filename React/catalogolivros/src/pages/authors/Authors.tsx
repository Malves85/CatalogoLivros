import "../../styles/Authors.css";
import { useState, useEffect } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import { Button, Card,} from "reactstrap";
import Col from "react-bootstrap/Col";
import Row from "react-bootstrap/Row";
import ReactPaginate from "react-paginate";
import "react-toastify/dist/ReactToastify.css";
import CardBody from "../../components/Card";
import { useNavigate } from "react-router-dom";
import { AuthorService } from "../../services/AuthorService";
import Toast from "../../helpers/Toast";

export default function Authors() {
    const [updateData, setUpdateData] = useState(true);
    //filtrar dados
    const [searchInput, setSearchInput] = useState("");
    //paginação
    const [pageCount, setPageCount] = useState(1);
    //ordenar
    const [sortValue, setSortValue] = useState("");
    const sortOptions = ["Nome", "País"];
    const [forcePage, setForcePage] = useState(0);
    const navigate = useNavigate();
    const [currentPage, setCurrentPage] = useState(0);
    const [pageSize, setPageSize] = useState(6);

    /*//Filtro
    const searchReset = async () => {
        setSearchInput("");
        setGetAuthors({
            ...getAuthors, searching:"", currentPage : 1
        })
        setForcePage(0);
        setUpdateData(true);
    };

    const searchAuthors = async (e: any) => {
        e.preventDefault();
        setGetAuthors({
            ...getAuthors, searching: searchInput, currentPage: 1,
        });
        setForcePage(0);
        setUpdateData(true);
  };

    const sortAuthors = async (e: any) => {
        let value = e.target.value;
        setSortValue(value);
        setGetAuthors({
            ...getAuthors, sorting: value,
        });
        setUpdateData(true);
    };*/
    //end

    //Busca todos os dados com a paginação
    /*const pedidoGet = async () => {

        const res = await (
            await axios.post(`${baseUrl}/getAuthors`, getAuthors)
        ).data;

        const total: number = res.totalRecords;

        setPageCount(res.totalPages);

        console.log("total " + total);

        await axios
        .post(`${baseUrl}/getAuthors`, getAuthors)
        .then((response) => {
            setData(response.data.items);
        })
        .catch((error) => {
            console.log(error);
        });
    };
    */

    const [authors, setAuthors] = useState([]);
    const authorService = new AuthorService();
    
    const loadAuthors = async () => {
        var response = await authorService.GetAll(currentPage, pageSize, searchInput, sortValue);
        
        if (response.success !== true) {
            Toast.Show("error", "Erro ao carregar os autores!");
            return;
        }

        if (response.items == null) {
            Toast.Show("error", "Não existem autores!");
            return;
        }
        setAuthors(response.items);
        setPageCount(response.totalPages);
        setUpdateData(true);
    };
    //end

    //Atualiza os dados da pagina
    useEffect(() => {
        if (updateData) {
            loadAuthors();
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
        <div className="Author-container">
            <br></br>
            <Row>
                <Col>
                    <Button style={{ backgroundColor:"darkgreen" }} 
                        onClick={() =>  navigate(`/createAuthor`)}>
                        Incluir novo autor
                    </Button>
                    <br></br>
                </Col>
                <Col></Col>
                <Col>
                    <h5>Ordenar por:</h5>
                </Col>
                <Col>
                    <select
                        style={{ width: "90px", borderRadius: "5px", height: "35px" }}
                        onChange={(e) => (setSortValue(e.target.value),setCurrentPage(0),setForcePage(0))}
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
                <form className="d-flex" role="search" >
                    <input
                        style={{ width: "250px", borderRadius: "2px", height: "35px" }}
                        className="form-control me-2 bg-light"
                        type="search"
                        placeholder="Buscar"
                        aria-label="Search"
                        value={searchInput}
                        onChange={(e) => (setSearchInput(e.target.value),setCurrentPage(0),setForcePage(0))}
                    />
                    {/*<Button style={{ backgroundColor: "blue" }} type="submit">
                        Ok
                    </Button>
                    <Button
                        style={{ backgroundColor: "red" }}
                        onClick={() => searchReset()}>
                        Resetar
                    </Button>*/}
                </form>
                </Col>
                <br></br>
                <br></br>
            </Row>

            {authors.length === 0 && searchInput.length > 2 ? (
                <Row xs={2 | 1} md={3} className="g-1">
                    <div className="justify-content-center">
                        <h4>Autor não encontrado</h4>
                    </div>
                </Row>
            ) : (
                <Row xs={2 | 1} md={3} className="g-3">
                    {authors.map((author: {
                        id: number;
                        name: string;
                        nacionality: string;
                        image: string;
                        authorTitle: [];
                    }) => (
                        <Col key={author.id} width="50px">
                            <Card
                                border="primary"
                                bg="light"
                                className="text-center"
                                style={{ width: "27rem" }}>
                                <CardBody
                                    isBook={false}
                                    name={author.name}
                                    nacionality={author.nacionality}
                                    image={author.image}
                                    authorTitle={author.authorTitle == null? "none"
                                    : author.authorTitle.join(", ")}
                                />
                        
                                <Row>
                                    <Col/>
                                        <Button
                                            style={{ width: "100px", height: "35px", backgroundColor: "blue" }}
                                            onClick={() => ( navigate(`/editAuthor/${author.id}`))}>
                                            Detalhes
                                        </Button>
                                        <br /><br />
                                    <Col/>
                                </Row>
                            </Card>
                        </Col>
                    ))}
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