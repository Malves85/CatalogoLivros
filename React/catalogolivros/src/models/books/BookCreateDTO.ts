import Joi from "joi";
import { GenericNotEmptySchema } from "../../helpers/JoiValidations";

export class BookCreateDTO {
    id: number = 0;
    isbn: number = 0;
    title: string = "";
    authorId: number = 0;
    price: number = 0;
    image: string = "";
}

export const CreateBookDTOSchema = Joi.object({
    isbn: Joi.number().min(0).messages({ "number.base": "Isbn deve ser um número", "number.min": "Isbn não pode ser inferior a 0" }),
    title: GenericNotEmptySchema("Título do livro"),
    authorId: Joi.number().min(0).messages({ "number.base": "Author deve estar preenchido" }),
    price: Joi.number().min(0).messages({ "number.base": "Preço deve ser um número", "number.min": "Preço não pode ser inferior a 0" }),
});