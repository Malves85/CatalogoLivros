import Joi from "joi";

export class BookCreateDTO {
    id: number = 0;
    isbn: number ;
    title: string = "";
    authorId: number = 0;
    price: number = 0;
    image: string = "";
}

export const CreateBookDTOSchema = Joi.object({
    isbn: Joi.number().min(0).required().messages({"any.required": "Isbn deve ser preenchido", "number.base": "Isbn deve ser preenchido", "number.min": "Isbn não pode ser inferior a 0" }),
    title: Joi.string().messages({"string.empty": "Título deve ser preenchido" }),
    authorId: Joi.number().min(1).required().messages({ "number.min": "Autor deve ser preenchido" }),
    price: Joi.number().min(0).required().messages({ "any.required": "Preço deve estar preenchido", "number.base": "Preço deve ser um número", "number.min": "Preço não pode ser inferior a 0" }),
});