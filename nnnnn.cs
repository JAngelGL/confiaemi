import NXOpen
import NXOpen.UF

def obtener_posicion_cara(face_select0):
    theSession = NXOpen.Session.GetSession()
    workPart = theSession.Parts.Work

    # Asumiendo que face_select0 ya tiene las caras recogidas
    faces = face_select0.GetEntities()
    
    for face in faces:
        # Usamos UFSession para acceder a funciones de bajo nivel
        ufSession = NXOpen.UF.UFSession.GetUFSession()
        
        # Obtener el tipo y los datos del cuerpo de la cara
        face_tag = face.Tag
        face_type, body_tag = ufSession.Modeling.AskFaceTypeAndBody(face_tag)
        
        # Calcular el centroide de la cara
        centroid = NXOpen.Point3d()
        area, centroid = ufSession.Modl.AskFaceArea(face_tag, centroid)
        
        # Imprimir la posición del centroide de la cara
        print("Centroide de la cara:", centroid)

# Asegúrate de llamar a la función con el FaceCollector adecuado
# obtener_posicion_cara(face_select0)
