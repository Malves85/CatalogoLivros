import Joi from "joi";

export class BookEditDTO {
    id: number = 0;
    isbn: number = 0;
    title: string = "";
    authorId: number = 0;
    price: number = 0;
    image: string = "";
}

export const EditBookDTOSchema = Joi.object({
    isbn: Joi.number().min(0).required().messages({"any.required": "Isbn deve ser preenchido", "number.base": "Isbn deve ser preenchido", "number.min": "Isbn não pode ser inferior a 0" }),
    title: Joi.string().messages({"string.empty": "Título deve ser preenchido" }),
    authorId: Joi.number().min(1).required().messages({ "number.base": "Autor deve ser preenchido" }),
    price: Joi.number().min(0).required().messages({ "number.base": "Preço deve ser preenchido", "number.min": "Preço não pode ser inferior a 0" }),
});