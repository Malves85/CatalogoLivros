import { useState } from "react";
import { Button, Col, Row } from "reactstrap";
import { AuthorService } from '../../services/AuthorService';
import { AuthorCreateDTO, CreateAuthorDTOSchema } from "../../models/authors/AuthorCreateDTO";
import { useNavigate } from 'react-router-dom';
import Toast from '../../helpers/Toast';
import "../../styles/AuthorCreate.css"
import Input from "../../components/Input";

export default function AuthorCreate() {
    const [author, setAuthor] = useState<AuthorCreateDTO>(new AuthorCreateDTO);
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
      var responseValidate = CreateAuthorDTOSchema.validate(author,{
          allowUnknown:true,   
      })
      console.log(responseValidate)
      if(responseValidate.error != null){
          var message = responseValidate.error!.message;
          Toast.Show("error",message);
          return
        }
      
          const response = await authorService.Create(author);

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