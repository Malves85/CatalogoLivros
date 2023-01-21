import { useState } from "react";
import { Col, Row } from "reactstrap";
import { BookService } from "../../services/BookService"
import { BookCreateDTO } from '../../models/books/BookCreateDTO';
import { BookDTO } from '../../models/books/BookDTO';
import { useNavigate } from 'react-router-dom';
import Toast from '../../helpers/Toast';
import "../../styles/BookCreate.css"
import Input from "../../components/Input";
import Select from "../../components/Select";
import Button from "../../components/Button";

export default function BookCreate() {
    const [book, setBook] = useState<BookDTO>({} as BookDTO);
    const navigate = useNavigate();
    const bookService = new BookService();
    const goBack = () => {navigate(-1)};
    
    const handleChange = (e: any) => {
        const { name, value } = e.target;
            setBook({
            ...book,
            [name]: value,
        });
    };

    const createBook = async()  => {
        const createBook : BookCreateDTO = {
            id: book.id,
            isbn: book.isbn,
            title: book.title,
            authorId: book.authorId,
            price: book.price,
            image: book.image,
        }
    
        const response = await bookService.Create(createBook);

        if (response.success !== true) {
            Toast.Show("error", response.message);
            return;
        }

        Toast.Show("success", response.message);
        goBack();

    }

    return (
        <Row className='newBookContainer'>
            <Col></Col>
      
            <Col className='border'>
                <br />
                <h2>Criar livro</h2>
                <div className="form-group" >

                    <Input
                    onChange={handleChange}
                    />

                    <Col>
                    <Button
                        style={{ backgroundColor: "red" }}
                        onClick={createBook}
                        label="Incluir"
                    />

                    <Button
                        style={{ backgroundColor: "red" }}
                        onClick={goBack}
                        label="Voltar"
                    />
                    
                    </Col>
                    
                    {/*<Button  style={{ backgroundColor:"blue" }} onClick={createBook}>
                        Incluir
                    </Button>{" "}

                    <Button style={{ backgroundColor:"red" }} onClick={goBack}>
                        Voltar
                    </Button>*/}

                </div>
            </Col>
            <Col></Col>
        </Row>
    )
}