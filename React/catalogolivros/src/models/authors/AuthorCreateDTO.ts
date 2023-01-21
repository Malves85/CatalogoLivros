import Joi from "joi";
import { GenericNotEmptySchema } from "../../helpers/JoiValidations";

export class AuthorCreateDTO {
    id: number = 0;
    name: string = "";
    nacionality: string = "";
    image: string = "";
}

export const CreateAuthorDTOSchema = Joi.object({
    name: GenericNotEmptySchema("Nome do autor"),
    nacionality: GenericNotEmptySchema("País do autor"),
    });