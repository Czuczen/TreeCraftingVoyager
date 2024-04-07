<template>
    <div>
        <!-- Navbar -->
        <nav class="navbar navbar-expand-lg navbar-light bg-light">
            <div class="container-fluid">
                <a class="navbar-brand" href="/">Tree Crafting Voyager</a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarNav">
                    <ul class="navbar-nav">
                        <li class="nav-item">
                            <router-link class="nav-link" to="/">Strona g³ówna</router-link>
                        </li>
                        <li class="nav-item">
                            <router-link class="nav-link" to="/logs">Logi</router-link>
                        </li>
                        <li class="nav-item">
                            <router-link class="nav-link" to="/about">O nas</router-link>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>

        <!-- Main content -->
        <div class="mt-4">
            <router-view></router-view>
        </div>

        <footer class="border-top footer text-muted">
            <div class="container-fluid">
                &copy; 2024 - Tree Crafting Voyager - <router-link to="/privacy">Prywatnoœæ</router-link>
            </div>
        </footer>
    </div>
</template>
