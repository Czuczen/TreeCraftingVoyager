<template>
    <div class="container">
        <header>
            <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
                <div class="container-fluid">
                    <a class="navbar-brand" href="/">TCV</a>
                    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                            aria-expanded="false" aria-label="Toggle navigation">
                        <span class="navbar-toggler-icon"></span>
                    </button>
                    <div class="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                        <ul class="navbar-nav flex-grow-1 justify-content-center">
                            <li class="nav-item">
                                <router-link class="nav-link text-dark" to="/">Strona główna</router-link>
                            </li>
                            <li class="nav-item dropdown">
                                <a class="nav-link dropdown-toggle text-dark" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                    Zadanie
                                </a>
                                <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
                                    <li><router-link class="dropdown-item" to="/categories">Kategorie</router-link></li>
                                    <li><hr class="dropdown-divider"></li>
                                    <li><router-link class="dropdown-item" to="/products">Produkty</router-link></li>
                                </ul>
                            </li>
                            <li class="nav-item">
                                <router-link class="nav-link text-dark" to="/about">O mnie</router-link>
                            </li>
                        </ul>
                        <div>
                            <router-link class="nav-link text-dark" to="/logs">Logi</router-link>
                        </div>
                    </div>
                </div>
            </nav>
        </header>

        <div class="container">
            <router-view></router-view>
        </div>

        <footer class="border-top footer text-muted mt-4">
            <div class="container">
                &copy; 2024 - Tree Crafting Voyager - <router-link to="/privacy">Prywatność</router-link>
            </div>
        </footer>
    </div>
</template>
