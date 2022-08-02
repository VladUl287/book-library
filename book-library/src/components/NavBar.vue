<template>
    <nav>
        <ul>
            <div class="logo">
                <router-link to="/">
                    <i class="bi bi-book"></i>
                    Library
                </router-link>
            </div>
            <div class="nav-items">
                <li>
                    <router-link to="/" class="nav-item">Главная</router-link>
                </li>
                <li>
                    <router-link to="/novelties" class="nav-item">Новинки</router-link>
                </li>
                <li>
                    <router-link to="/recommendations" class="nav-item">Рекомендации</router-link>
                </li>
                <li>
                    <router-link to="/collections" class="nav-item">Коллекции</router-link>
                </li>
            </div>
            <li class="nav-account">
                <div class="nav-wrap">
                    <label for="profile-check" class="profile-icon">
                        <i class="bi bi-person-circle"></i>
                    </label>
                    <input type="checkbox" name="profile-check" id="profile-check">
                    <div class="profile-select">
                        <div>
                            <router-link to="/profile">
                                <i class="bi bi-person"></i>
                                ulyanovskiy.01@mail.ru
                            </router-link>
                        </div>
                        <div>
                            <router-link to="/">
                                <i class="bi bi-collection"></i>
                                Мои коллекции
                            </router-link>
                        </div>
                        <div>
                            <router-link to="/read-list">
                                <i class="bi bi-journal-text"></i>
                                Прочитанные
                            </router-link>
                        </div>
                        <div>
                            <router-link to="/bookmarks">
                                <i class="bi bi-bookmark-check"></i>
                                Закладки
                            </router-link>
                        </div>
                        <div>
                            <router-link to="#" @click="logout">
                                <i class="bi bi-box-arrow-left"></i>
                                Выход
                            </router-link>
                        </div>
                    </div>
                </div>
            </li>
        </ul>
    </nav>
</template>

<script setup lang="ts">
import { authModule } from '@/store/modules/auth';
import { useRouter } from 'vue-router';

const router = useRouter()

const logout = async () => {
    await authModule.Logout();
    router.push('auth');
}
</script>

<style>
nav {
    width: 100%;
    padding: 0 70px;
    background-color: transparent;
}

nav ul {
    margin: 0;
    display: grid;
    grid-template-columns: 1fr auto 1fr;
    padding: 15px 0;
    column-gap: .5em;
    list-style: none;
    align-items: center;
    justify-content: center;
}

nav ul .logo a {
    display: block;
    margin: 0 auto 0 0;
    font-size: 25px;
    color: #f1f1f1;
    text-align: left;
    text-decoration: none;
    text-transform: uppercase;
}

nav ul .nav-items {
    display: flex;
    column-gap: 2em;
}

nav ul .nav-item {
    display: block;
    cursor: pointer;
    color: #f1f1f1;
    user-select: none;
    position: relative;
    text-decoration: none;
    padding: .7em .1em .7em .1em;
}

nav ul .nav-item::after {
    content: '';
    position: absolute;
    left: 0;
    bottom: 0;
    width: 100%;
    height: 4px;
    background-color: red;
    transform: scaleX(0);
    transform-origin: left;
    transition: transform 150ms ease-in-out;
}

nav ul .nav-item:hover::after {
    transform: scaleX(1)
}

nav .nav-account {
    position: relative;
}

nav .nav-account .nav-wrap {
    width: fit-content;
    margin: 0 0 0 auto;
}

nav .nav-account .profile-icon {
    cursor: pointer;
    font-size: xx-large;
    background-color: transparent;
}

nav .nav-account #profile-check {
    display: none;
}

nav .nav-account .profile-select {
    display: none;
    position: absolute;
    right: 0;
    top: 100%;
    padding: .5em;
    z-index: 9999;
    width: fit-content;
    user-select: none;
    background-color: #f1f1f1;
}

nav .nav-account .profile-select a {
    width: 100%;
    color: #000;
    padding: .5em;
    display: block;
    text-align: left;
    text-decoration: none;
}

nav .nav-account .profile-select a:hover,
nav .nav-account .profile-select a:focus {
    background-color: rgba(128, 128, 128, 0.2);
}

nav .nav-account #profile-check:checked~.profile-select {
    display: block;
}
</style>