import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Button, Col, Row } from "reactstrap";
import Input from "../../components/Input";
import Toast from "../../helpers/Toast";
import { BookDTO } from "../../models/books/BookDTO";
import { BookEditDTO, EditBookDTOSchema } from "../../models/books/BookEditDTO";
import { BookService } from "../../services/BookService";
import "../../styles/BookEdit.css";

export default function BookEdit() {
    const { id } = useParams<{ id: string }>();
    const navigate = useNavigate();
    const [book, setBook] = useState<BookDTO>({} as BookDTO);
    const bookService = new BookService();

    const handleChange = (e: any) => {
        const { name, value } = e.target;
        setBook({
        ...book,
        [name]: value,
        });
    };

    useEffect(() => {
        loadBook(parseInt(id));
    }, [id]);

    const loadBook = async (id: number) => {
        var response = await bookService.GetById(id);

        if (response.success !== true) {
            Toast.Show("error", "Erro ao carregar o livro!");
            return;
        }

        if (response.obj == null) {
            Toast.Show("error", "livro não existe!");
            return;
        }
        setBook(response.obj);
    };

    const updateBook = async () => {
      var responseValidate = EditBookDTOSchema.validate(book,{
        allowUnknown:true,
    })
    console.log("autor "+book.authorId)
    if(responseValidate.error != null){
        var message = responseValidate.error!.message;
        Toast.Show("error",message);
        return
      }
        const updatedBook: BookEditDTO = {
          
            id: parseInt(id),
            isbn: book.isbn,
            title: book.title,
            price: book.price,
            image: book.image,
            authorId: book.authorId,
        };

        const response = await bookService.Edit(updatedBook);

        if (response.success !== true) {
            Toast.Show("error", response.message);
            return;
        }

        Toast.Show("success", response.message);
        goBack();
    };

    const deleteBook = async (id: number) => {
        const response = await bookService.DeleteBook(id);

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
    <Row className="editBookContainer">
      <Col></Col>

      <Col className="border">
        <br />
        <h3>Detalhes</h3>
        <div className="form-group">
          <label>Id </label>
          <input type="number" className="form-control" readOnly value={id} />
          <br />
          
          <Input
              
                isBook={true}
                id={book && book.authorId}
                onChange={handleChange}
                isbn={book && book.isbn}
                title={book && book.title}
                price={book && book.price}
            />
          <Button
            style={{ marginLeft: "80px", backgroundColor: "blue" }}
            onClick={updateBook}
          >
            Salvar
          </Button>{" "}
          <Button style={{ backgroundColor: "darkgreen" }} onClick={goBack}>
            Voltar
          </Button>{" "}
          <Button
            style={{ marginLeft: "80px", backgroundColor: "red" }}
            onClick={() => deleteBook(parseInt(id))}
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
