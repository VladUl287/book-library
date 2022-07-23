import { store } from "..";
import instance from "@/http";
import { Genre } from "@/common/contracts";
import { Module, VuexModule, Mutation, Action, getModule } from "vuex-module-decorators";

@Module({ dynamic: true, store: store, name: 'booksModule', preserveState: localStorage.getItem('vuex') !== null })
class GenresModule extends VuexModule {
    private _genres: Genre[] = [];

    get genres(): Genre[] {
        return this._genres
    }

    @Mutation
    setGenres(genres: Genre[]) {
        this._genres = genres;
    }

    @Action
    async getGenres() {
        const result = await instance.get<Genre[]>('genre/getAll')
        this.setGenres(result.data)
    }
}

export const genresModule = getModule(GenresModule, store)