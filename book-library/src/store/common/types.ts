import { Guid } from "guid-typescript"

export type PageFilter = {
    page?: number,
    size?: number,
}

export type BooksFilter = PageFilter & {
    name?: string,
    genres?: Guid[],
    rating?: number,
    beginYear?: number,
    endYear?: number
}

export type CollectionFilter = PageFilter & {
    viewsSort?: boolean,
}

export type ReviewFilter = PageFilter & {
    viewsSort?: boolean,
}