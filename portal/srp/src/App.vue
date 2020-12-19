<template>
  <v-app>
    <v-app-bar app dark color="blue-grey darken-2" rounded="true">
      <v-toolbar-title class="ml-2">
        Scanf Rules
      </v-toolbar-title>
    </v-app-bar>
    <v-main>
      <v-container>
        <v-row>
          <v-col cols="3"><v-card>Rule List</v-card></v-col>
          <v-divider></v-divider>
          <v-col cols="9"><v-card>Rule Description</v-card></v-col>
        </v-row>
      </v-container>
    </v-main>
  </v-app>
</template>

<script>
import Navbar from "./components/generic/Navbar.vue";
export default {
  name: "App",
  component: { Navbar },
};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 10px;
}

#app > .row {
  min-height: 100vh;
}

.flex-fill {
  flex: 1 1 auto;
}
</style>
