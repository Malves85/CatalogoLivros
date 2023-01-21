import Joi from "joi";
import { GenericNotEmptySchema } from "../../helpers/JoiValidations";

export class AuthorEditDTO {
    id: number = 0;
    name: string = "";
    nacionality: string = "";
    image: string = "";
}

export const EditAuthorDTOSchema = Joi.object({
    name: GenericNotEmptySchema("Nome do autor"),
    nacionality: GenericNotEmptySchema("País do autor"),
    });