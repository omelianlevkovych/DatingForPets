FROM node:14.15.1-alpine AS build
WORKDIR /usr/src/app

# Installing dependencies. If you already have built the image once it'll take less time for next builds.
COPY package*.json /usr/src/app/
RUN npm install
RUN npm install -g @angular/cli@11.0.2

# Building the app.
COPY . .
RUN ng build

FROM nginx:1.19.5-alpine
COPY nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /usr/src/app/dist/client/ /srv/myapp/