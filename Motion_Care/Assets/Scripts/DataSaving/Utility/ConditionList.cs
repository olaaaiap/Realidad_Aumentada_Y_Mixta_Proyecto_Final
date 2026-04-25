using System.Collections.Generic;
using UnityEngine;

public static class ConditionList
{
    public static List<Profile> randomProfiles = new List<Profile>()
    {
        new ("Ansiedad generalizada", "Preocupacion constante, tension y dificultad para relajarse."),
        new ("Depresion mayor", "Estado de animo bajo, apatia, fatiga y perdida de interes."),
        new ("Trastorno del sueño", "Dificultad para conciliar o mantener el sueño."),
        new ("Aislamiento social", "Poca interaccion social y sensacion de soledad."),
        new ("Duelo prolongado", "Tristeza persistente tras perdida de seres queridos."),
        new ("Irritabilidad", "Cambios frecuentes de humor con baja tolerancia a la frustracion."),

        new ("Deterioro cognitivo leve", "Problemas leves de memoria y concentracion."),
        new ("Demencia", "Deterioro progresivo de memoria, lenguaje y juicio."),
        new ("Perdida de memoria reciente", "Olvido frecuente de eventos recientes."),
        new ("Desorientacion temporal", "Dificultad para ubicarse en el tiempo."),
        new ("Desorientacion espacial", "Dificultad para reconocer lugares conocidos."),
        new ("Confusion ocasional", "Episodios breves de desorientacion o pensamiento poco claro."),

        new ("Bradicinesia", "Lentitud en los movimientos voluntarios."),
        new ("Inestabilidad postural", "Dificultad para mantener el equilibrio."),
        new ("Marcha inestable", "Forma de caminar insegura o irregular."),
        new ("Temblor", "Movimiento involuntario ritmico."),
        new ("Rigidez muscular", "Resistencia al movimiento pasivo."),
        new ("Caidas frecuentes", "Perdida recurrente del equilibrio con riesgo de lesiones."),
        new ("Debilidad muscular", "Reduccion de fuerza en extremidades."),
        new ("Coordinacion reducida", "Dificultad para realizar movimientos precisos."),

        new ("Artritis", "Inflamacion articular con dolor y rigidez."),
        new ("Osteoporosis", "Fragilidad osea con alto riesgo de fracturas."),
        new ("Dolor cronico", "Dolor persistente que afecta la calidad de vida."),
        new ("Hipertension", "Presion arterial elevada sostenida."),
        new ("Diabetes tipo 2", "Alteracion en el control de glucosa en sangre."),
        new ("Problemas de vision", "Disminucion de la agudeza visual."),
        new ("Perdida auditiva", "Disminucion de la capacidad para oir."),
        new ("Fatiga cronica", "Cansancio persistente no aliviado con descanso."),
        new ("Incontinencia urinaria", "Perdida involuntaria de orina."),

        new ("Fragilidad geriatrica", "Disminucion global de la resistencia fisica."),
        new ("Polifarmacia", "Uso simultaneo de multiples medicamentos."),
        new ("Desnutricion", "Ingesta insuficiente de nutrientes."),
        new ("Deshidratacion", "Falta de liquidos en el organismo."),
        new ("Movilidad reducida", "Limitacion para desplazarse de forma independiente."),
        new ("Dependencia funcional", "Necesidad de ayuda para actividades diarias."),

        new ("Soledad cronica", "Sensacion persistente de estar solo o desconectado."),
        new ("Miedo a caerse", "Inseguridad constante al caminar o moverse."),
        new ("Perdida de autonomia", "Dificultad para realizar tareas cotidianas sin ayuda."),
        new ("Falta de apetito", "Disminucion del interes por la comida."),
        new ("Problemas digestivos", "Molestias frecuentes en el sistema digestivo."),
        new ("Respiracion dificultosa", "Sensacion de falta de aire en actividades leves."),
        new ("Baja energia", "Sensacion constante de cansancio o debilidad."),
        new ("Problemas de equilibrio", "Dificultad para mantenerse estable al estar de pie."),
        new ("Rigidez articular", "Limitacion en el rango de movimiento de las articulaciones."),
        new ("Lentitud cognitiva", "Procesamiento mental mas lento de lo habitual.")
    };

    public static Profile GetRandomCondition()
    {
        int index = Random.Range(0, randomProfiles.Count);
        return randomProfiles[index];
    }

}
