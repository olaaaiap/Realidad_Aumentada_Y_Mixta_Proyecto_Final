# Motion Care

**Experiencia de realidad mixta para la rehabilitación física y cognitiva de personas mayores.**

Proyecto final de la asignatura de Realidad Aumentada y Mixta del máster. Motion Care convierte una habitación cualquiera en un espacio interactivo con minijuegos basados en tareas cotidianas (poner la mesa, recordar secuencias, preparar bocadillos…) para trabajar la motricidad fina, la coordinación, la memoria y la movilidad de las extremidades superiores, en un entorno seguro, accesible y monitorizado.

[![Vídeo de demostración](https://img.youtube.com/vi/fBF2XPzVjmY/hqdefault.jpg)](https://youtu.be/fBF2XPzVjmY)

## Enlaces

- 🎥 **Vídeo de demostración:** [youtu.be/fBF2XPzVjmY](https://youtu.be/fBF2XPzVjmY)
- 🎨 **Diseño de la interfaz de la app web (Figma):** [Prototipo MotionCare](https://www.figma.com/proto/c8iVcXS6IfpSk5nd7xLjA1/MotionCare-basic-design?node-id=501-2&p=f&t=Ds008WutCUNPzM2l-0&scaling=contain&content-scaling=fixed&page-id=0%3A1&starting-point-node-id=501%3A2)
- 📄 **Memoria del proyecto:** [Memoria MotionCare.pdf](Memoria%20MotionCare.pdf)

## Motivación

La población envejece y, con ella, aumenta la necesidad de espacios seguros y supervisados para hacer ejercicio. Motion Care propone usar la realidad mixta para que, con unas gafas y un acompañante, cualquier persona mayor pueda iniciar una rutina de ejercicios físicos y mentales desde casa, sin depender de instalaciones especializadas. El objetivo no es el alto rendimiento, sino ofrecer una forma agradable y familiar de trabajar la motricidad, la coordinación y la memoria.

## Recorrido de la experiencia

La aplicación guía al usuario por una vivienda virtual de forma secuencial, con instrucciones por voz y feedback sonoro en cada acción:

| Estancia | Qué se pide | Capacidades trabajadas |
|---|---|---|
| **Tutorial** | Llevar una manzana al bol, guardar libros en una caja y pulsar un botón para encender la TV | Aprendizaje de agarre, colocación y pulsación |
| **Comedor** | Poner la mesa en tres niveles de dificultad y servir agua en una copa | Motricidad fina y memoria |
| **Sala de estar** | *Simón dice* con libros y tazas, y juego de emparejar cartas | Memoria y secuenciación |
| **Cocina** | Montar bocadillos siguiendo una receta que solo se muestra unos segundos, con dificultad adaptativa | Memoria, coordinación y movilidad |

## Mi aportación: el Comedor

Diseñé e implementé la sala del **Comedor**, donde el usuario debe poner la mesa colocando los cubiertos en su sitio. La prueba se divide en tres niveles:

- **Fácil:** los cubiertos aparecen cerca de sus guías dibujadas sobre la mesa.
- **Media:** misma mecánica, pero con los cubiertos en posiciones menos intuitivas.
- **Difícil:** desaparecen las guías visuales, por lo que el usuario tiene que recordar dónde va cada cubierto. Al terminar, aparece una botella con la que debe llenar una copa.

Aspectos técnicos destacados:

- **Sistema de colocación por slots:** al soltar un cubierto cerca de su posición correcta, se recoloca automáticamente en su sitio y deja de ser interactuable, reforzando la sensación de acierto (`ReleaseDetector`, `PlacementManager`, `PlacementSlot`, `TableObject`).
- **Simulación de vertido de líquido:** la botella genera gotas cuando su inclinación supera los 45°; la copa calcula cuánto líquido cae dentro y fuera, y un cilindro sube según el porcentaje de llenado (`BottlePour`, `CupVolume`, `LiquidVisual`, `Drop`).
- **Registro de métricas:** el resultado del vertido se guarda en el JSON de sesión a través del `DataManager` común.
- **Gestión de la sala:** transición entre niveles, reaparición de objetos caídos y reproducción de audios guía (`GameManagerComedor`, `RespawnWhenFarComedor`).

Todos los scripts de esta sala están en [`Motion_Care/Assets/Scripts/Comedor`](Motion_Care/Assets/Scripts/Comedor).

## Arquitectura general

- **Desarrollo modular:** cada estancia es un prefab independiente construido sobre un prefab base común (mismas escalas y referencias espaciales), lo que permitió trabajar en paralelo por ramas e integrar todo sin conflictos.
- **GameManager central:** activa y desactiva cada estancia en orden, con transiciones suaves de escala mediante tweening.
- **Recogida de datos:** un `DataManager` (singleton) centraliza usuario, sesiones, tareas y métricas por tipo de ejercicio (movimiento, memoria, secuencia, vertido) y las exporta a JSON. Un `ChronometerManager` (singleton) mide los tiempos de cada actividad.
- **Diseño accesible:** estilo visual *cartoon* con colores cálidos, objetos resaltados con contorno, instrucciones por voz y feedback inmediato para reducir la incertidumbre en usuarios poco familiarizados con la tecnología.

## Tecnologías

- **Unity 6** (6000.3) con Universal Render Pipeline
- **Meta XR SDK** (Core, Interaction, MR Utility Kit) — Grab, Poke y Snap Interactables
- **C#**
- **DOTween** para animaciones y transiciones
- **Figma** para el mock-up de la plataforma web
- **ElevenLabs** para los audios de instrucciones

## Trabajo futuro

- Plataforma web para que cuidadores y profesionales consulten la ficha de cada paciente y la evolución de sus métricas con gráficos (ver [prototipo en Figma](https://www.figma.com/proto/c8iVcXS6IfpSk5nd7xLjA1/MotionCare-basic-design?node-id=501-2&p=f&t=Ds008WutCUNPzM2l-0&scaling=contain&content-scaling=fixed&page-id=0%3A1&starting-point-node-id=501%3A2)).
- Nuevos minijuegos y métricas.
- Personalización de la sesión por parte del cuidador según las necesidades diarias de cada paciente.

## Equipo

| Integrante | Estancia |
|---|---|
| Manuel Alejandro Villalba | Tutorial |
| **Olaia Picabea** | **Comedor** |
| Hilario Javier Del Valle | Sala de estar |
| Pablo Álvarez de Lara | Cocina |
