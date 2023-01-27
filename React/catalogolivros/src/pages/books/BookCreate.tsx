import { useState } from "react";
import { Button, Col, Row } from "reactstrap";
import { BookService } from "../../services/BookService"
import { BookCreateDTO, CreateBookDTOSchema } from '../../models/books/BookCreateDTO';
import { useNavigate } from 'react-router-dom';
import Toast from '../../helpers/Toast';
import "../../styles/BookCreate.css"
import Input from "../../components/Input";

export default function BookCreate() {
    const [book, setBook] = useState<BookCreateDTO>(new BookCreateDTO());
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
        var responseValidate = CreateBookDTOSchema.validate(book,{
            allowUnknown:true,
        })
        console.log("author "+book.authorId)
        if(responseValidate.error != null){
            var message = responseValidate.error!.message;
            Toast.Show("error",message);
            return
          }
        
            const response = await bookService.Create(book);

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
                    isBook={true}
                    onChange={handleChange}
                    />

                    <Button  style={{ backgroundColor:"blue" }} onClick={createBook}>
                        Incluir
                    </Button>{" "}

                    <Button style={{ backgroundColor:"red" }} onClick={goBack}>
                        Voltar
                    </Button>
                    <br /> <br />
                </div>
            </Col>
            <Col></Col>
        </Row>
    )
}