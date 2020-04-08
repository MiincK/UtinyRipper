namespace uTinyRipper.BundleFiles
{
	public sealed class BundleFileEntry : FileEntry, IBundleReadable
	{
		public static bool HasBlobIndex(BundleType type, BundleGeneration generation)
		{
			// Hack: fix UnityFS <530 gen parsing
			return type == BundleType.UnityFS || generation >= BundleGeneration.BF_530_x;
		}

		public void Read(BundleReader reader)
		{
			if (HasBlobIndex(reader.Type, reader.Generation))
			{
				Offset = reader.ReadInt64();
				Size = reader.ReadInt64();
				BlobIndex = reader.ReadInt32();
				NameOrigin = reader.ReadStringZeroTerm();
			}
			else
			{
				NameOrigin = reader.ReadStringZeroTerm();
				Offset = reader.ReadInt32();
				Size = reader.ReadInt32();
			}
			Name = FilenameUtils.FixFileIdentifier(NameOrigin);
		}

		public int BlobIndex { get; private set; }
	}
}
