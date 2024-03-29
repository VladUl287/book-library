import { getUrlParams } from './../common/helpers';
import { BooksFilter } from './../common/types';
import { store } from "..";
import instance from "@/http";
import { Book } from "@/common/contracts";
import { Module, VuexModule, Mutation, Action, getModule } from "vuex-module-decorators";
import { Guid } from "guid-typescript";

@Module({ dynamic: true, store: store, name: 'booksModule', preserveState: localStorage.getItem('vuex') !== null })
class BooksModule extends VuexModule {
    private _books: Book[] = []
    private _selectedBook: Book | undefined
    private _filters: BooksFilter = {};

    get books(): Book[] {
        return this._books
    }

    get filters(): BooksFilter {
        return this._filters
    }

    get selectedBook(): Book | undefined {
        return this._selectedBook
    }

    @Mutation
    setBooks(books: Book[]) {
        this._books = books
    }

    @Mutation
    setFilters(filters: BooksFilter) {
        this._filters = filters
    }

    @Mutation
    selectBook(book: Book | undefined) {
        this._selectedBook = book
    }

    @Action
    async getBooks() {
        const params = getUrlParams(this._filters);
        const result = await instance.get<Book[]>('book/getAll', { params })
        this.setBooks(result.data)
    }

    @Action
    async getNoveltiesBooks(): Promise<Book[]> {
        const result = await instance.get<Book[]>('book/getNoveltiesBooks')
        return result.data;
    }

    @Action
    async getBooksByCollection(id: Guid): Promise<Book[]> {
        const result = await instance.get<Book[]>('book/getByCollection/' + id)
        return result.data;
    }

    @Action
    async getBooksByAuthor(id: Guid): Promise<Book[]> {
        const result = await instance.get<Book[]>('book/getByAuthor/' + id)
        return result.data;
    }

    @Action
    async getBookById(id: Guid): Promise<void> {
        const result = await instance.get<Book>('book/getById/' + id)
        this.selectBook(result.data);
    }

    @Action
    async getRecommendations(): Promise<Book[]> {
        const result = await instance.get<Book[]>('book/getRecommendations')
        return result.data;
    }

    @Action
    async getReadBooks(): Promise<Book[]> {
        const result = await instance.get<Book[]>('book/getReadBooks')
        return result.data;
    }

    @Action
    async markAsRead(id: Guid): Promise<void> {
        await instance.patch('book/markAsRead/' + id)
    }
}

export const booksModule = getModule(BooksModule, store)