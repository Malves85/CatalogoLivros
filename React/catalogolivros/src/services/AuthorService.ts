import { MessagingHelper } from "../helpers/MessagingHelper";
import { PaginatedList } from "../helpers/PaginatedList";
import { AuthorListDTO } from "../models/authors/AuthorListDTO";
import { APIService } from "./APIService";
import { AuthorCreateDTO } from "../models/authors/AuthorCreateDTO";
import { AuthorDTO } from "../models/authors/AuthorDTO";
import { AuthorEditDTO } from "../models/authors/AuthorEditDTO";

export class AuthorService {
    async GetAll(
        currentPage: number,
        pageSize: number,
        searching: string,
        sorting: string,
    ): Promise<PaginatedList<AuthorListDTO>> {
        try {
            var response = await APIService.Axios().post(
                `${APIService.GetURL()}/Authors/getAuthors`,
                {
                    currentPage,
                    pageSize,
                    searching,
                    sorting,

                },
                {
                    headers: {
                        Accept: "application/json",
                        "Content-Type": "application/json",
                    },
                },
            );
            return response.data;
        } catch (error) {
            return new PaginatedList<AuthorListDTO>(
                false,
                "Erro ao obter a informação dos autores",
                "",
                [],
                0,
            );
        }
    }

    async Create(task: AuthorCreateDTO): Promise<MessagingHelper<null>> {
        try {
            var response = await APIService.Axios().post(
                `${APIService.GetURL()}/Authors/create`,
                { ...task },
                {
                    headers: {
                        Accept: "application/json",
                        "Content-Type": "application/json",
                    },
                },
            );
            return response.data;
        } catch (error) {
            return new MessagingHelper(
                false,
                "Erro ao ligar ao servidor para criar autor!",
                null,
            );
        }
    }

    async GetById(id: number): Promise<MessagingHelper<AuthorDTO | null>> {
        try {
            var response = await APIService.Axios().get(`${APIService.GetURL()}/Authors/${id}`, {
                headers: {
                    Accept: "application/json",
                    "Content-Type": "application/json",
                },
            });
            return response.data;
        } catch (error) {
            return new MessagingHelper(
                false,
                "Erro ao ligar ao servidor para ir buscar informação do autor!",
                null,
            );
        }
    }

    async Edit(task: AuthorEditDTO): Promise<MessagingHelper<AuthorDTO | null>> {
        try {
            var response = await APIService.Axios().post(
                `${APIService.GetURL()}/Authors/update`,
                { ...task },
                {
                    headers: {
                        Accept: "application/json",
                        "Content-Type": "application/json",
                    },
                },
            );
            return response.data;
        } catch (error) {
            return new MessagingHelper<AuthorDTO | null>(
                false,
                "Erro ao ligar ao servidor para editar o autor",
                null,
            );
        }
    }

    async DeleteAuthor(id: number): Promise<MessagingHelper<null>> {
        try {
            var response = await APIService.Axios().post(
                `${APIService.GetURL()}/Authors/delete`,
                {
                    id: id
                },
                {
                    headers: {
                        Accept: "application/json",
                        "Content-Type": "application/json",
                    },
                },
            );
            return response.data;
        } catch (error) {
            return new MessagingHelper(
                false,
                "Erro ao ligar ao servidor para deletar autor",
                null,
            );
        }
    }

}
