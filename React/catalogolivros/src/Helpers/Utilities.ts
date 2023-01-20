import { SortingRule } from "react-table";
import { Parameter } from "../models/Parameter";
import { PageURL } from "./PageURL";

class UtilitiesClass {


    //Método para verificar se valor é inteiro
    public IsInteger(value: any): boolean {

        //Significa que tem caracteres de uma string, logo é falso
        if (/^(\-|\+)?([0-9]+)$/.test(value) === false) return false;

        var valueInNumber = Number(value);
        return Number.isInteger(valueInNumber) && (Math.abs(valueInNumber) < 2147483647);
    }

    //Método para verificar se valor é data
    public IsDate(value: any): boolean {
        switch (typeof value) {
            case 'number':
                return true;
            case 'string':
                return !isNaN(Date.parse(value));
            case 'object':
                if (value instanceof Date) {
                    return !isNaN(value.getTime());
                } else {
                    return false;
                }
            default:
                return false;
        }
    }

    //Método para retonar valor default se undefined ou a null
    public IfIsUndefinedOrNullThen(value: any, defaultValue: any) {
        if (typeof value == 'undefined' || value == null) return defaultValue;
        return value;
    }

    //Método para adicionar um query parameter ao URL
    public AddParamToQueryIfValue(queryParamName: string, type: "string" | "number" | "date", value: any): string {
        if (typeof value == 'undefined' || value == null) return '';

        //Se o tipo do parâmetro que queremos for string
        if (type === "string") {
            //Tentamos ver se a string tem alguma coisa
            if (value.trim() === "") return "";
        }

        if (type === "date") {
            if (this.IsDate(value) === false) return "";
        }

        if (type === "number") {
            if (this.IsInteger(value) === false) return "";
        }

        return `&${queryParamName}=${value}`;
    }

    //Método para obter um parâmetro dos parâmetros do URL convertido já como o valor correto    
    public LoadParameterFromURLQuery(parameterName: string, validateParameterType: "string" | "date" | "number", defaultValue: any): any {
        try {
            var searchParams = new URLSearchParams(PageURL.GetSearchParameters());

            var parameterExist = searchParams.get(parameterName);
            if (parameterExist == null) return defaultValue;

            switch (validateParameterType) {
                case "date":

                    //É uma data
                    if (this.IsDate(parameterExist) === true) {
                        return parameterExist;
                    } else {
                        return defaultValue;
                    }

                case "number":
                    if (this.IsInteger(parameterExist) === true) {
                        return parseInt(parameterExist);
                    } else {
                        return defaultValue;
                    }

                case "string":
                    if (typeof parameterExist == 'undefined' || parameterExist == null || parameterExist.trim().length <= 0) return defaultValue;
                    return parameterExist.toString();
            }

        } catch (exp) {
            return defaultValue;
        }
    }

    //Método para carregar o tamanho da página pelo URL
    public LoadPageSizeFromURLQuery(pageSizes: number[], defaultPageSize?: number): number {

        var searchParams = new URLSearchParams(PageURL.GetSearchParameters());
        //Obtemos query parameter pageSize
        var tmpPageSize = searchParams.get("pageSize");

        var normalPageSize: number = typeof defaultPageSize !== 'undefined' ? defaultPageSize : 5;

        //Se não existir colocamos valor default 5
        var futurePageSize: number = tmpPageSize == null ? normalPageSize : parseInt(tmpPageSize);

        //Se o valor que o utilizador escolheu não estiver na listagem permitida, colocamos a primeira posição do valor
        if (!pageSizes.includes(futurePageSize)) {
            futurePageSize = pageSizes[0];
        }

        return futurePageSize;
    }

    //Método para carregar a página do URL
    public LoadPageFromURLQuery(pageParamName: string): number {

        //Obtemos o query parameter page
        var searchParams = new URLSearchParams(PageURL.GetSearchParameters());
        var tmpPage = searchParams.get(pageParamName);

        //Se parâmetro não existe, mandamos 1
        if (tmpPage == null) return 1;

        var futurePage: number = 1;
        if (this.IsInteger(tmpPage) === true) {
            futurePage = parseInt(tmpPage ?? "1");
        }

        if (futurePage <= 0) {
            futurePage = 1;
        }

        return futurePage;
    }

    public LoadSortParametersFromURLQuery(sortFieldName: string, sortOrderName: string): SortingRule<object>[] {
        var sortParameter: SortingRule<object>[] = []

        try {

            //Tentar obter o parâmetro com o nome do campo de ordenação, se campo não existir saimos
            var sortField = this.LoadParameterFromURLQuery(sortFieldName, "string", null);
            if (sortField == null) return sortParameter;

            //Se campo de ordenação existir tentamos obter, a ordem, se não existir colocamos o valor ASC como default
            var sortOrder = this.LoadParameterFromURLQuery(sortOrderName, "string", "ASC");

            sortParameter.push({ id: sortField, desc: sortOrder === "DESC" ? true : false });
        } catch (exp) {
            sortParameter = [];
        }
        return sortParameter;
    }

    public IsNullOrEmpty = (value: string | null | undefined): boolean => {
        try {
            if (typeof value == "undefined") return true;
            if (value == null) return true;
            if (typeof value !== "string") return true;
            if (value.trim().length <= 0) return true;
            return false;
        } catch (exp) {
            return true;
        }
    }

    public DaysBetweenDates = (initDate: string, endDate: string): number => {
        try {
            if (this.IsNullOrEmpty(initDate) || this.IsNullOrEmpty(endDate)) return 0;

            var firstDate = Date.parse(initDate);
            var secondDate = Date.parse(endDate);

            if (isNaN(firstDate) || isNaN(secondDate)) return 0;

            var differenceInMs = secondDate - firstDate;
            var differenceInDays = Math.round(differenceInMs / (86400000));

            return differenceInDays;
        } catch (exp) {
            return 0;
        }
    }

    public MountSortParameters = (sortBy: SortingRule<object>[] | null): Parameter[] => {
        var parameters: Parameter[] = [];

        if (sortBy == null) return parameters;

        if (sortBy.length > 0) {
            parameters.push(new Parameter(sortBy[0].id, sortBy[0].desc === true ? "DESC" : "ASC"));
        }

        return parameters;
    }

    public CreateSearchURL(parameters: string[]): string {
        var url: string = "?";
        var isFirstParameter: boolean = true;

        for (let i = 0; i < parameters.length; i++) {
            var parameter: string = parameters[i];
            var parameterAlreadyHasAndInIt: boolean = parameter.startsWith("&");

            if (this.IsNullOrEmpty(parameter) === false) {
                if (isFirstParameter === true) {

                    if (parameterAlreadyHasAndInIt === true) {
                        parameter = parameter.replace("&", "");
                    }
                    isFirstParameter = false;
                } else {
                    if (parameterAlreadyHasAndInIt === false) {
                        parameter = "&" + parameter;
                    }
                }

                url += parameter;
            }
        }

        return url;
    }

    public GetDateFromDateTime(dateTime: string): string {
        var isInvalidDate = isNaN(Date.parse(dateTime));
        if (isInvalidDate) return dateTime;
        var date = new Date(dateTime);
        return date.toISOString().split("T")[0];
    }

    public GetTimeFromDateTime(dateTime: string): string {
        var isInvalidDate = isNaN(Date.parse(dateTime));
        if (isInvalidDate) return dateTime;

        var date = new Date(dateTime);
        var hours: string | number = date.getHours();
        var minutes: string | number = date.getMinutes();

        hours = (hours <= 9) ? '0' + hours : hours;
        minutes = (minutes <= 9) ? '0' + minutes : minutes;

        return `${hours}:${minutes}`;
    }
}

export const Utilities = new UtilitiesClass();