import Joi from "joi";

export class AuthorCreateDTO {
    id: number = 0;
    name: string = "";
    nacionality: string = "";
    image: string = "";
}

export const CreateAuthorDTOSchema = Joi.object({
    name: Joi.string().messages({"string.empty": "Nome do autor deve ser preenchido" }),
    nacionality: Joi.string().messages({"string.empty": "País deve ser preenchido" })
});