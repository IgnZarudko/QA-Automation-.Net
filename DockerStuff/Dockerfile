FROM tomcat:9.0-alpine

RUN ["rm", "-rf", "/usr/local/tomcat/webapps/ROOT/*"]

ADD ./docker_task /usr/local/tomcat/webapps/ROOT

EXPOSE 8080

CMD ["catalina.sh", "run"]