import { useState } from "react";
import { Button, Col, Row } from "reactstrap";
import Toast from '../../helpers/Toast';
import { useNavigate } from 'react-router-dom';
import "../../styles/AuthorCreate.css"
import { AuthorService } from '../../services/AuthorService';
import { AuthorDTO } from "../../models/authors/AuthorDTO";
import { AuthorCreateDTO } from "../../models/authors/AuthorCreateDTO";
import Input from "../../components/Input";


export default function AuthorCreate() {
    const [author, setAuthor] = useState<AuthorDTO>({} as AuthorDTO);
    const navigate = useNavigate();
    const authorService = new AuthorService();
    const goBack = () => {navigate(-1)};
    
    const handleChange = (e: any) => {
        const { name, value } = e.target;
            setAuthor({
            ...author,
            [name]: value,
        });
    };

    const createAuthor = async()  => {
        const createAuthor : AuthorCreateDTO = {
            id: author.id,
            name: author.name,
            nacionality: author.nacionality,
            image: author.image,
        }
    
        const response = await authorService.Create(createAuthor);

        if (response.success !== true) {
            Toast.Show("error", response.message);
            return;
        }

        Toast.Show("success", response.message);
        goBack();

    }

    return (
        <Row className="newAuthorContainer">
          <Col></Col>
    
          <Col className="border">
            <br />
            <h2>Criar Autor</h2>
            <div className="form-group">
            <Input
              
              isBook={false}
              onChange={handleChange}
            />
              <Button  style={{ backgroundColor:"blue" }} onClick={createAuthor}>
                        Incluir
                </Button>{" "}

                <Button style={{ backgroundColor:"red" }} onClick={goBack}>
                    Voltar
                </Button>
              <br />
              <br />
            </div>
          </Col>
          <Col></Col>
        </Row>
      );
    }