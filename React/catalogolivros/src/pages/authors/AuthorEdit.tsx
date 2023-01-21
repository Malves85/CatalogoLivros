import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Button, Col, Row } from "reactstrap";
import Toast from "../../helpers/Toast";
import { AuthorDTO } from "../../models/authors/AuthorDTO";
import { AuthorEditDTO } from "../../models/authors/AuthorEditDTO";
import { AuthorService } from "../../services/AuthorService";
import "../../styles/AuthorEdit.css";

export default function AuthorEdit() {
    const { id } = useParams<{ id: string }>();
    const navigate = useNavigate();
    const [author, setAuthor] = useState<AuthorDTO>({} as AuthorDTO);
    const authorService = new AuthorService();

    const handleChange = (e: any) => {
        const { name, value } = e.target;
        setAuthor({
        ...author,
        [name]: value,
        });
    };

    useEffect(() => {
        loadAuthors(parseInt(id));
    }, [id]);

    const loadAuthors = async (id: number) => {
        var response = await authorService.GetById(id);

        if (response.success !== true) {
            Toast.Show("error", "Erro ao carregar o autor!");
            return;
        }

        if (response.obj == null) {
            Toast.Show("error", "Autor não existe!");
            return;
        }
        setAuthor(response.obj);
    };

    const updateAuthor = async () => {
        const updatedAuthor: AuthorEditDTO = {
            id: parseInt(id),
            name : author.name,
            nacionality: author.nacionality,
            image: author.image,
        };

        const response = await authorService.Edit(updatedAuthor);

        if (response.success !== true) {
            Toast.Show("error", response.message);
            return;
        }

        Toast.Show("success", response.message);
        goBack();
    };

    const deleteAuthor = async (id: number) => {
        const response = await authorService.DeleteAuthor(id);

        if (response.success !== true) {
            Toast.Show("error", response.message);
            return;
        }

        Toast.Show("success", response.message);
        goBack();
    };

    const goBack = () => {
        navigate(-1);
    };

  return (
    <Row className="editAuthorContainer">
      <Col></Col>

      <Col className="border">
        <br />
        <h2>Detalhes</h2>
        <div className="form-group">
          <label>Id </label>
          <input type="number" className="form-control" readOnly value={id} />
          <br />
          <label>Nome </label>
          <br />
          <input
            type="text"
            className="form-control"
            name="name"
            onChange={handleChange}
            value={author && author.name}
          />
          <br />
          <label>País </label>
          <br />
          <input
            type="text"
            className="form-control"
            name="nacionality"
            onChange={handleChange}
            value={author && author.nacionality}
          />
          <br />
          <label>Imagem </label>
          <br />
          <input
            type="text"
            className="form-control"
            name="image"
            onChange={handleChange}
            value={author && author.image}
          />
          <br />
          <Button
            style={{ marginLeft: "80px", backgroundColor: "blue" }}
            onClick={updateAuthor}
          >
            Salvar
          </Button>{" "}
          <Button style={{ backgroundColor: "darkgreen" }} onClick={goBack}>
            Voltar
          </Button>{" "}
          <Button
            style={{ marginLeft: "80px", backgroundColor: "red" }}
            onClick={() => deleteAuthor(parseInt(id))}
          >
            Excluir
          </Button>
          <br />
          <br />
        </div>
      </Col>
      <Col></Col>
    </Row>
  );
}