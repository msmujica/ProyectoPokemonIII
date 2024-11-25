# ProyectoPokemon-III

## Desafíos más difíciles
Uno de los desafíos más complicados del proyecto fue la implementación de efectos, ya que era un área en la que tuvimos que pensar varias veces para encontrar la 
mejor manera de hacerlo. Sin embargo, lo más difícil no fue eso, sino tener que replantear todo el código solo dos días antes de la entrega. 
La forma en que estábamos abordando el problema en relación con los Ataques y Tipos estaba equivocada, por lo que tuvimos que hacer ajustes significativos. 
Para resolver esto, seguimos los consejos del profesor Sebastián, lo que nos ayudó a reencaminar el desarrollo.

Otro desafío importante fue la implementación de los comandos de Pokémon en el contexto de Discord. Al intentar integrar los comandos en Discord y entender 
cómo funciona la interacción con la API de Discord, tuvimos dificultades para conceptualizar cómo organizar las acciones del Pokémon y su interacción con los comandos. 
Implementar comandos como "atacar", "ver pokemones disponibles", "listar ataques", etc., esto requería cómo gestionarlo 
dentro de un bot de Discord. 
La documentación y la estructura de Discord, aunque potente, tiene una curva de aprendizaje considerable, especialmente cuando se trata de 
manejar interacciones entre el bot y los usuarios de forma eficiente.

## Aprendizajes fuera de la clase
En este proyecto aprendimos varias cosas que no habíamos cubierto previamente en clase, pero que fueron esenciales para el desarrollo. 
Por ejemplo, el uso de diccionarios en programación fue algo nuevo para nosotros, ya que no habíamos utilizado esta estructura en los ejercicios anteriores. 
Además, otro aspecto que nos costó entender fue la implementación del operador ? en el siguiente fragmento de código:
Entrenador? player = this.WaitingList.FindTrainerByDisplayName(playerDisplayName);
Este operador tiene que ver con la operación de nulabilidad en C#. Específicamente, se usa para indicar que la variable player puede ser de tipo Entrenador o null. 
Esto nos permitió trabajar con datos que podrían no existir sin causar errores en el código.

## Recursos útiles
Durante el desarrollo, utilizamos diversos recursos para superar los obstáculos encontrados. Algunas de las fuentes más útiles fueron:

StackOverflow, donde encontramos soluciones a varios problemas técnicos.
Documentación de Microsoft sobre C#, que fue esencial para resolver dudas específicas sobre sintaxis y mejores prácticas.
DoxyGen, donde encontramos un guia de comandos a utilizar.
GitHub, donde encontramos una guia para realizar este archivo ReadMe
Y, por supuesto, los profesores, quienes nos ofrecieron orientación clave para replantear el enfoque del proyecto y pensarlo de manera diferente.

## Reflexión final
En general, creemos que este proyecto es una excelente demostración de lo que hemos aprendido hasta ahora. Nos permitió aplicar los principios y 
conceptos que hemos estudiado en clase y entender cómo se integran en un proyecto real.
Sin embargo, también reconocemos que uno de los aspectos más desafiantes fue imaginar cómo implementar el proyecto como un chatbot en Discord. 
Además, sería muy útil que nos brindaran más orientación sobre cómo implementar un bot de Discord desde cero. A pesar de haber estudiado, 
aún nos queda mucha cosas que no logramos comprender, y una guía o ejemplos adicionales sobre cómo integrar el bot con nuestro juego de Pokémon nos 
ayudaría a superar las barreras más rápidamente.
En conclusión, estamos satisfechos con el trabajo realizado, y creemos que este proyecto es un excelente ejemplo de aprendizaje y aplicación práctica.
