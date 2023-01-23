import Joi from "joi";

export class AuthorEditDTO {
    id: number = 0;
    name: string = "";
    nacionality: string = "";
    image: string = "";
}

export const EditAuthorDTOSchema = Joi.object({
    name: Joi.string().messages({"string.empty": "Nome do autor deve ser preenchido" }),
    nacionality: Joi.string().messages({"string.empty": "País deve ser preenchido" })
});